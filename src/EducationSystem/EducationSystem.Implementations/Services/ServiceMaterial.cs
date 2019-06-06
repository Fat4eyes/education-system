using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Extensions;
using EducationSystem.Helpers;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Interfaces.Services;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Rest;
using EducationSystem.Specifications.Materials;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Services
{
    public class ServiceMaterial : Service<ServiceMaterial>, IServiceMaterial
    {
        private readonly IValidator<Material> _validatorMaterial;
        private readonly IRepository<DatabaseMaterial> _repositoryMaterial;
        private readonly IRepository<DatabaseMaterialFile> _repositoryMaterialFile;
        private readonly IRepository<DatabaseMaterialAnchor> _repositoryMaterialAnchor;

        public ServiceMaterial(
            IMapper mapper,
            IContext context,
            ILogger<ServiceMaterial> logger,
            IValidator<Material> validatorMaterial,
            IRepository<DatabaseMaterial> repositoryMaterial,
            IRepository<DatabaseMaterialFile> repositoryMaterialFile,
            IRepository<DatabaseMaterialAnchor> repositoryMaterialAnchor)
            : base(mapper, context, logger)
        {
            _validatorMaterial = validatorMaterial;
            _repositoryMaterial = repositoryMaterial;
            _repositoryMaterialFile = repositoryMaterialFile;
            _repositoryMaterialAnchor = repositoryMaterialAnchor;
        }

        public async Task<PagedData<Material>> GetMaterialsAsync(FilterMaterial filter)
        {
            var user = await Context.GetCurrentUserAsync();

            if (user.IsAdmin())
            {
                var specification = new MaterialsByName(filter.Name);

                var (count, materials) = await _repositoryMaterial.FindPaginatedAsync(specification, filter);

                return new PagedData<Material>(Mapper.Map<List<Material>>(materials), count);
            }

            if (user.IsLecturer())
            {
                var specification =
                    new MaterialsByName(filter.Name) &
                    new MaterialsByOwnerId(user.Id);

                var (count, materials) = await _repositoryMaterial.FindPaginatedAsync(specification, filter);

                return new PagedData<Material>(Mapper.Map<List<Material>>(materials), count);
            }

            throw ExceptionHelper.NoAccess();
        }

        public async Task<Material> GetMaterialAsync(int id)
        {
            // Студент сейчас может получить доступ к любому материалу любого преподавателя.
            // Под материалом здесь понимается учебный материал.

            // TODO: Исправить это при необходимости.

            var user = await Context.GetCurrentUserAsync();

            if (user.IsAdmin() || user.IsStudent())
            {
                var material = await _repositoryMaterial.FindFirstAsync(new MaterialsById(id)) ??
                    throw ExceptionHelper.NotFound<DatabaseMaterial>(id);

                return Mapper.Map<Material>(material);
            }

            if (user.IsLecturer())
            {
                var material = await _repositoryMaterial.FindFirstAsync(new MaterialsById(id)) ??
                    throw ExceptionHelper.NotFound<DatabaseMaterial>(id);

                if (new MaterialsByOwnerId(user.Id).IsSatisfiedBy(material) == false)
                    throw ExceptionHelper.NoAccess();

                return Mapper.Map<Material>(material);
            }

            throw ExceptionHelper.NoAccess();
        }

        public async Task DeleteMaterialAsync(int id)
        {
            var user = await Context.GetCurrentUserAsync();

            if (user.IsNotAdmin() && user.IsNotLecturer())
                throw ExceptionHelper.NoAccess();

            var material = await _repositoryMaterial.FindFirstAsync(new MaterialsById(id)) ??
                throw ExceptionHelper.NotFound<DatabaseMaterial>(id);

            if (user.IsNotAdmin() && !new MaterialsByOwnerId(user.Id).IsSatisfiedBy(material))
                throw ExceptionHelper.NoAccess();

            await _repositoryMaterial.RemoveAsync(material, true);
        }

        public async Task UpdateMaterialAsync(int id, Material material)
        {
            var user = await Context.GetCurrentUserAsync();

            if (user.IsNotLecturer())
                throw ExceptionHelper.NoAccess();

            await _validatorMaterial.ValidateAsync(material.Format());

            var model = await _repositoryMaterial.FindFirstAsync(new MaterialsById(id)) ??
                throw ExceptionHelper.NotFound<DatabaseMaterial>(id);

            if (new MaterialsByOwnerId(user.Id).IsSatisfiedBy(model) == false)
                throw ExceptionHelper.NoAccess();

            Mapper.Map(Mapper.Map<DatabaseMaterial>(material), model);

            await _repositoryMaterial.UpdateAsync(model, true);

            if (model.Files.Any())
                await _repositoryMaterialFile.RemoveAsync(model.Files, true);

            model.Files = Mapper.Map<List<DatabaseMaterialFile>>(material.Files);

            await ProcessAnchorsAsync(material, model);
        }

        public async Task<int> CreateMaterialAsync(Material material)
        {
            var user = await Context.GetCurrentUserAsync();

            if (user.IsNotLecturer())
                throw ExceptionHelper.NoAccess();

            await _validatorMaterial.ValidateAsync(material.Format());

            var model = Mapper.Map<DatabaseMaterial>(material);

            model.OwnerId = user.Id;

            await _repositoryMaterial.AddAsync(model, true);

            return model.Id;
        }

        private async Task ProcessAnchorsAsync(Material material, DatabaseMaterial model)
        {
            await RemoveAnchorsAsync(material, model);
            await UpdateAnchorsAsync(material, model);
            await InsertAnchorsAsync(material, model);

            await _repositoryMaterial.UpdateAsync(model, true);
        }

        private async Task RemoveAnchorsAsync(Material material, DatabaseMaterial model)
        {
            if (material.Anchors.IsEmpty())
            {
                if (model.Anchors.IsNotEmpty())
                    await _repositoryMaterialAnchor.RemoveAsync(model.Anchors, true);

                return;
            }

            var ids = material.Anchors
                .Select(x => x.Id)
                .ToArray();

            var anchors = model.Anchors
                .Where(x => !ids.Contains(x.Id))
                .ToArray();

            if (anchors.IsNotEmpty())
                await _repositoryMaterialAnchor.RemoveAsync(anchors, true);
        }

        private async Task UpdateAnchorsAsync(Material material, DatabaseMaterial model)
        {
            if (material.Anchors.IsEmpty())
                return;

            var ids = material.Anchors
                .Select(x => x.Id)
                .ToArray();

            MaterialAnchor GetAnchor(int id)
            {
                return material.Anchors.First(y => y.Id == id);
            }

            var anchors = model.Anchors
                .Where(x => ids.Contains(x.Id))
                .Select(x => Mapper.Map(GetAnchor(x.Id), x))
                .ToArray();

            if (anchors.IsNotEmpty())
                await _repositoryMaterialAnchor.UpdateAsync(anchors, true);
        }

        private async Task InsertAnchorsAsync(Material material, DatabaseMaterial model)
        {
            if (material.Anchors.IsEmpty())
                return;

            var ids = model.Anchors
                .Select(x => x.Id)
                .ToArray();

            var anchors = material.Anchors
                .Where(x => !ids.Contains(x.Id))
                .Select(Mapper.Map<DatabaseMaterialAnchor>)
                .ToList();

            if (anchors.IsEmpty())
                return;

            anchors.ForEach(x => x.MaterialId = model.Id);

            await _repositoryMaterialAnchor.AddAsync(anchors, true);

            model.Anchors.AddRange(anchors);
        }
    }
}