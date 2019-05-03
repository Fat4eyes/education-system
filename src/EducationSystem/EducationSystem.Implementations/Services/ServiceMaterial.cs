using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Implementations.Specifications;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Interfaces.Helpers;
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
        private readonly IExecutionContext _executionContext;
        private readonly IHelperUserRole _helperUserRole;
        private readonly IValidator<Material> _validatorMaterial;
        private readonly IRepository<DatabaseMaterial> _repositoryMaterial;
        private readonly IRepository<DatabaseMaterialFile> _repositoryMaterialFile;
        private readonly IExceptionFactory _exceptionFactory;

        public ServiceMaterial(
            IMapper mapper,
            IExecutionContext executionContext,
            IHelperUserRole helperUserRole,
            ILogger<ServiceMaterial> logger,
            IValidator<Material> validatorMaterial,
            IRepository<DatabaseMaterial> repositoryMaterial,
            IRepository<DatabaseMaterialFile> repositoryMaterialFile,
            IExceptionFactory exceptionFactory)
            : base(mapper, logger)
        {
            _executionContext = executionContext;
            _helperUserRole = helperUserRole;
            _validatorMaterial = validatorMaterial;
            _repositoryMaterial = repositoryMaterial;
            _repositoryMaterialFile = repositoryMaterialFile;
            _exceptionFactory = exceptionFactory;
        }

        public async Task<PagedData<Material>> GetMaterialsAsync(FilterMaterial filter)
        {
            var specification = new MaterialsByName(filter.Name);

            var (count, materials) = await _repositoryMaterial.FindPaginatedAsync(specification, filter);

            return new PagedData<Material>(Mapper.Map<List<Material>>(materials), count);
        }

        public async Task<PagedData<Material>> GetLecturerMaterialsAsync(int lecturerId, FilterMaterial filter)
        {
            await _helperUserRole.CheckRoleLecturerAsync(lecturerId);

            var specification =
                new MaterialsByName(filter.Name) &
                new MaterialsByOwnerId(lecturerId);

            var (count, materials) = await _repositoryMaterial.FindPaginatedAsync(specification, filter);

            return new PagedData<Material>(Mapper.Map<List<Material>>(materials), count);
        }

        public async Task<Material> GetMaterialAsync(int id)
        {
            var material = await _repositoryMaterial.FindFirstAsync(new MaterialsById(id)) ??
                throw _exceptionFactory.NotFound<DatabaseMaterial>(id);

            return Mapper.Map<Material>(material);
        }

        public async Task<Material> GetStudentMaterialAsync(int id, int studentId)
        {
            await _helperUserRole.CheckRoleStudentAsync(studentId);

            // Студент сейчас может получить доступ к любому материалу любого преподавателя (администратора).
            // Под материалом здесь понимается учебный материал.

            // TODO: Исправить это при необходимости.

            var material = await _repositoryMaterial.FindFirstAsync(new MaterialsById(id)) ??
                throw _exceptionFactory.NotFound<DatabaseMaterial>(id);

            return Mapper.Map<Material>(material);
        }

        public async Task<Material> GetLecturerMaterialAsync(int id, int lecturerId)
        {
            await _helperUserRole.CheckRoleLecturerAsync(lecturerId);

            var specification =
                new MaterialsById(id) &
                new MaterialsByOwnerId(lecturerId);

            var material = await _repositoryMaterial.FindFirstAsync(specification) ??
                throw _exceptionFactory.NotFound<DatabaseMaterial>(id);

            return Mapper.Map<Material>(material);
        }

        public async Task DeleteMaterialAsync(int id)
        {
            var material = await _repositoryMaterial.FindFirstAsync(new MaterialsById(id)) ??
                throw _exceptionFactory.NotFound<DatabaseMaterial>(id);

            var user = await _executionContext.GetCurrentUserAsync();

            // Удалить материал может только администратор или владелец материала.
            if (user.IsNotAdmin() && !new MaterialsByOwnerId(user.Id).IsSatisfiedBy(material))
                throw _exceptionFactory.NoAccess();

            await _repositoryMaterial.RemoveAsync(material, true);
        }

        public async Task UpdateMaterialAsync(int id, Material material)
        {
            await _validatorMaterial.ValidateAsync(material.Format());

            var model = await _repositoryMaterial.FindFirstAsync(new MaterialsById(id)) ??
                throw _exceptionFactory.NotFound<DatabaseMaterial>(id);

            var user = await _executionContext.GetCurrentUserAsync();

            // Изменить материал может только владелец материала.
            if (!new MaterialsByOwnerId(user.Id).IsSatisfiedBy(model))
                throw _exceptionFactory.NoAccess();

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

            var user = await _executionContext.GetCurrentUserAsync();

            model.OwnerId = user.Id;

            await _repositoryMaterial.AddAsync(model, true);

            return model.Id;
        }
    }
}