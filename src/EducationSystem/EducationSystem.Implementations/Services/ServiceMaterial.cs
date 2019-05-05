using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Implementations.Specifications;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Interfaces.Services;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Rest;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Services
{
    public class ServiceMaterial : Service<ServiceMaterial>, IServiceMaterial
    {
        private readonly IValidator<Material> _validatorMaterial;
        private readonly IRepository<DatabaseMaterial> _repositoryMaterial;
        private readonly IRepository<DatabaseMaterialFile> _repositoryMaterialFile;

        public ServiceMaterial(
            IMapper mapper,
            IExecutionContext executionContext,
            ILogger<ServiceMaterial> logger,
            IValidator<Material> validatorMaterial,
            IRepository<DatabaseMaterial> repositoryMaterial,
            IRepository<DatabaseMaterialFile> repositoryMaterialFile,
            IExceptionFactory exceptionFactory)
            : base(
                mapper,
                logger,
                executionContext,
                exceptionFactory)
        {
            _validatorMaterial = validatorMaterial;
            _repositoryMaterial = repositoryMaterial;
            _repositoryMaterialFile = repositoryMaterialFile;
        }

        public async Task<PagedData<Material>> GetMaterialsAsync(FilterMaterial filter)
        {
            if (CurrentUser.IsAdmin())
            {
                var specification = new MaterialsByName(filter.Name);

                var (count, materials) = await _repositoryMaterial.FindPaginatedAsync(specification, filter);

                return new PagedData<Material>(Mapper.Map<List<Material>>(materials), count);
            }

            if (CurrentUser.IsLecturer())
            {
                var specification =
                    new MaterialsByName(filter.Name) &
                    new MaterialsByOwnerId(CurrentUser.Id);

                var (count, materials) = await _repositoryMaterial.FindPaginatedAsync(specification, filter);

                return new PagedData<Material>(Mapper.Map<List<Material>>(materials), count);
            }

            throw ExceptionFactory.NoAccess();
        }

        public async Task<Material> GetMaterialAsync(int id)
        {
            // Студент сейчас может получить доступ к любому материалу любого преподавателя (администратора).
            // Под материалом здесь понимается учебный материал.

            // TODO: Исправить это при необходимости.

            if (CurrentUser.IsAdmin() || CurrentUser.IsStudent())
            {
                var material = await _repositoryMaterial.FindFirstAsync(new MaterialsById(id)) ??
                    throw ExceptionFactory.NotFound<DatabaseMaterial>(id);

                return Mapper.Map<Material>(material);
            }

            if (CurrentUser.IsLecturer())
            {
                var material = await _repositoryMaterial.FindFirstAsync(new MaterialsById(id)) ??
                    throw ExceptionFactory.NotFound<DatabaseMaterial>(id);

                if (new MaterialsByOwnerId(CurrentUser.Id).IsSatisfiedBy(material) == false)
                    throw ExceptionFactory.NoAccess();

                return Mapper.Map<Material>(material);
            }

            throw ExceptionFactory.NoAccess();
        }

        public async Task DeleteMaterialAsync(int id)
        {
            var material = await _repositoryMaterial.FindFirstAsync(new MaterialsById(id)) ??
                throw ExceptionFactory.NotFound<DatabaseMaterial>(id);

            if (CurrentUser.IsNotAdmin() && !new MaterialsByOwnerId(CurrentUser.Id).IsSatisfiedBy(material))
                throw ExceptionFactory.NoAccess();

            await _repositoryMaterial.RemoveAsync(material, true);
        }

        public async Task UpdateMaterialAsync(int id, Material material)
        {
            await _validatorMaterial.ValidateAsync(material.Format());

            var model = await _repositoryMaterial.FindFirstAsync(new MaterialsById(id)) ??
                throw ExceptionFactory.NotFound<DatabaseMaterial>(id);

            if (!new MaterialsByOwnerId(CurrentUser.Id).IsSatisfiedBy(model))
                throw ExceptionFactory.NoAccess();

            Mapper.Map(Mapper.Map<DatabaseMaterial>(material), model);

            await _repositoryMaterial.UpdateAsync(model, true);

            if (model.Files.Any())
                await _repositoryMaterialFile.RemoveAsync(model.Files, true);

            model.Files = Mapper.Map<List<DatabaseMaterialFile>>(material.Files);

            await _repositoryMaterialFile.AddAsync(model.Files, true);
        }

        public async Task<int> CreateMaterialAsync(Material material)
        {
            await _validatorMaterial.ValidateAsync(material.Format());

            var model = Mapper.Map<DatabaseMaterial>(material);

            model.OwnerId = CurrentUser.Id;

            await _repositoryMaterial.AddAsync(model, true);

            return model.Id;
        }
    }
}