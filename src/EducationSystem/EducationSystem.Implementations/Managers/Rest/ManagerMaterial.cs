using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Exceptions.Helpers;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Managers.Rest;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models;
using EducationSystem.Models.Files;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.Models.Rest;
using EducationSystem.Models.Rest.Basics;
using EducationSystem.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Managers.Rest
{
    public class ManagerMaterial : Manager<ManagerMaterial>, IManagerMaterial
    {
        private readonly IHelperPath _helperPath;
        private readonly IValidator<Material> _validatorMaterial;
        private readonly IRepositoryMaterial _repositoryMaterial;
        private readonly IRepositoryMaterialFile _repositoryMaterialFile;

        public ManagerMaterial(
            IMapper mapper,
            ILogger<ManagerMaterial> logger,
            IHelperPath helperPath,
            IValidator<Material> validatorMaterial,
            IRepositoryMaterial repositoryMaterial,
            IRepositoryMaterialFile repositoryMaterialFile)
            : base(mapper, logger)
        {
            _helperPath = helperPath;
            _validatorMaterial = validatorMaterial;
            _repositoryMaterial = repositoryMaterial;
            _repositoryMaterialFile = repositoryMaterialFile;
        }

        public PagedData<Material> GetMaterials(OptionsMaterial options, FilterMaterial filter)
        {
            var (count, materials) = _repositoryMaterial.GetMaterials(filter);

            return new PagedData<Material>(materials.Select(Map).ToList(), count);
        }

        public async Task DeleteMaterialByIdAsync(int id)
        {
            var material = _repositoryMaterial.GetById(id) ??
                throw ExceptionHelper.CreateNotFoundException(
                    $"Материал для удаления не найден. Идентификатор материала: {id}.",
                    $"Материал для удаления не найден.");

            await _repositoryMaterial.RemoveAsync(material, true);
        }

        public Material GetMaterialById(int id)
        {
            var material = _repositoryMaterial.GetById(id) ??
                throw ExceptionHelper.CreateNotFoundException(
                    $"Материал не найден. Идентификатор материала: {id}.",
                    $"Материал не найден.");

            return Map(material);
        }

        public async Task<Material> CreateMaterialAsync(Material material)
        {
            _validatorMaterial.Validate(material);

            FormatMaterial(material);

            var model = Mapper.Map<DatabaseMaterial>(material);

            await _repositoryMaterial.AddAsync(model, true);

            return Map(model);
        }

        public async Task<Material> UpdateMaterialAsync(int id, Material material)
        {
            _validatorMaterial.Validate(material);

            var model = _repositoryMaterial.GetById(id) ??
                throw ExceptionHelper.CreateNotFoundException(
                    $"Материал для обновления не найден. Идентификатор материала: {id}.",
                    $"Материал для обновления не найден.");

            FormatMaterial(material);

            Mapper.Map(Mapper.Map<DatabaseMaterial>(material), model);

            await _repositoryMaterial.UpdateAsync(model, true);

            if (model.Files.Any())
                await _repositoryMaterialFile.RemoveAsync(model.Files, true);

            model.Files = Mapper.Map<List<DatabaseMaterialFile>>(material.Files);

            await _repositoryMaterialFile.AddAsync(model.Files, true);

            return Map(model);
        }

        private Material Map(DatabaseMaterial material)
        {
            return Mapper.Map<DatabaseMaterial, Material>(material, x =>
            {
                x.AfterMap((s, d) =>
                {
                    d.Files?.ForEach(y => y.Path = GetDocumentPath(d, y));
                });
            });
        }

        private string GetDocumentPath(Model material, Document document)
        {
            if (document == null)
                throw new ArgumentNullException(nameof(document));

            if (document.Guid.HasValue == false)
                throw ExceptionHelper.CreateException(
                    $"Не удалось получить документ материала. " +
                        $"Идентификатор материала: {material.Id}. " +
                        $"Идентификатор документа: {document.Id}.",
                    $"Не удалось получить документ материала.");

            return _helperPath
                .GetRelativeFilePath(document.Type, document.Guid.Value, document.Name)
                .Replace("\\", "/");
        }

        private static void FormatMaterial(Material material)
        {
            material.Name = material.Name.Trim();
            material.Template = material.Template.Trim();
        }
    }
}