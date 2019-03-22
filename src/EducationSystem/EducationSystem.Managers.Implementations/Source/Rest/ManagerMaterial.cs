﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Database.Models.Source;
using EducationSystem.Exceptions.Source.Helpers;
using EducationSystem.Extensions.Source;
using EducationSystem.Helpers.Interfaces.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Managers.Implementations.Source.Rest
{
    public class ManagerMaterial : Manager<ManagerMaterial>, IManagerMaterial
    {
        private readonly IHelperMaterial _helperMaterial;
        private readonly IRepositoryMaterial _repositoryMaterial;
        private readonly IRepositoryMaterialFile _repositoryMaterialFile;

        public ManagerMaterial(
            IMapper mapper,
            ILogger<ManagerMaterial> logger,
            IHelperMaterial helperMaterial,
            IRepositoryMaterial repositoryMaterial,
            IRepositoryMaterialFile repositoryMaterialFile)
            : base(mapper, logger)
        {
            _helperMaterial = helperMaterial;
            _repositoryMaterial = repositoryMaterial;
            _repositoryMaterialFile = repositoryMaterialFile;
        }

        public PagedData<Material> GetMaterials(OptionsMaterial options, FilterMaterial filter)
        {
            var (count, materials) = _repositoryMaterial.GetMaterials(filter);

            return new PagedData<Material>(Mapper.Map<List<Material>>(materials), count);
        }

        public void DeleteMaterialById(int id)
            => DeleteMaterialByIdAsync(id).WaitTask();

        public async Task DeleteMaterialByIdAsync(int id)
        {
            var material = _repositoryMaterial.GetById(id) ??
                throw ExceptionHelper.CreateNotFoundException(
                    $"Материал для удаления не найден. Идентификатор материала: {id}.",
                    $"Материал для удаления не найден.");

            await _repositoryMaterial.RemoveAsync(material, true);
        }

        public Material CreateMaterial(Material material)
            => CreateMaterialAsync(material).WaitTask();

        public async Task<Material> CreateMaterialAsync(Material material)
        {
            _helperMaterial.ValidateMaterial(material);

            FormatMaterial(material);

            var model = Mapper.Map<DatabaseMaterial>(material);

            await _repositoryMaterial.AddAsync(model, true);

            return Mapper.Map<DatabaseMaterial, Material>(model);
        }

        public Material UpdateMaterial(int id, Material material)
            => UpdateMaterialAsync(id, material).WaitTask();

        public async Task<Material> UpdateMaterialAsync(int id, Material material)
        {
            _helperMaterial.ValidateMaterial(material);

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

            return Mapper.Map<DatabaseMaterial, Material>(model);
        }

        private static void FormatMaterial(Material material)
        {
            material.Name = material.Name.Trim();
            material.Template = material.Template.Trim();
        }
    }
}