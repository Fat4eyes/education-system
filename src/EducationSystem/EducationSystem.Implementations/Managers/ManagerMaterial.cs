using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Managers;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models;
using EducationSystem.Models.Files;
using EducationSystem.Models.Files.Basics;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.Models.Rest;
using EducationSystem.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Managers
{
    public class ManagerMaterial : Manager<ManagerMaterial>, IManagerMaterial
    {
        private readonly IHelperPath _helperPath;
        private readonly IValidator<Material> _validatorMaterial;
        private readonly IRepositoryMaterial _repositoryMaterial;
        private readonly IRepositoryMaterialFile _repositoryMaterialFile;
        private readonly IExceptionFactory _exceptionFactory;

        public ManagerMaterial(
            IMapper mapper,
            ILogger<ManagerMaterial> logger,
            IHelperPath helperPath,
            IValidator<Material> validatorMaterial,
            IRepositoryMaterial repositoryMaterial,
            IRepositoryMaterialFile repositoryMaterialFile,
            IExceptionFactory exceptionFactory)
            : base(mapper, logger)
        {
            _helperPath = helperPath;
            _validatorMaterial = validatorMaterial;
            _repositoryMaterial = repositoryMaterial;
            _repositoryMaterialFile = repositoryMaterialFile;
            _exceptionFactory = exceptionFactory;
        }

        public async Task<PagedData<Material>> GetMaterialsAsync(OptionsMaterial options, FilterMaterial filter)
        {
            var (count, materials) = await _repositoryMaterial.GetMaterialsAsync(filter);

            var items = materials
                .Select(x => Map(x, options))
                .ToList();

            return new PagedData<Material>(items, count);
        }

        public async Task DeleteMaterialAsync(int id)
        {
            var material = await _repositoryMaterial.GetByIdAsync(id) ??
                throw _exceptionFactory.NotFound<DatabaseMaterial>(id);

            await _repositoryMaterial.RemoveAsync(material, true);
        }

        public async Task<Material> GetMaterialAsync(int id, OptionsMaterial options)
        {
            var material = await _repositoryMaterial.GetByIdAsync(id) ??
                throw _exceptionFactory.NotFound<DatabaseMaterial>(id);

            return Map(material, options);
        }

        public async Task<Material> CreateMaterialAsync(Material material)
        {
            await _validatorMaterial.ValidateAsync(material.Format());

            var model = Mapper.Map<DatabaseMaterial>(material);

            await _repositoryMaterial.AddAsync(model, true);

            return Mapper.Map<DatabaseMaterial, Material>(model);
        }

        public async Task<Material> UpdateMaterialAsync(int id, Material material)
        {
            await _validatorMaterial.ValidateAsync(material.Format());

            var model = await _repositoryMaterial.GetByIdAsync(id) ??
                throw _exceptionFactory.NotFound<DatabaseMaterial>(id);

            Mapper.Map(Mapper.Map<DatabaseMaterial>(material), model);

            await _repositoryMaterial.UpdateAsync(model, true);

            if (model.Files.Any())
                await _repositoryMaterialFile.RemoveAsync(model.Files, true);

            model.Files = Mapper.Map<List<DatabaseMaterialFile>>(material.Files);

            await _repositoryMaterialFile.AddAsync(model.Files, true);

            return Mapper.Map<DatabaseMaterial, Material>(model);
        }

        private Material Map(DatabaseMaterial material, OptionsMaterial options)
        {
            return Mapper.Map<DatabaseMaterial, Material>(material, x =>
            {
                x.AfterMap((s, d) =>
                {
                    if (options.WithFiles)
                    {
                        var files = s.Files?.Select(y => y.File).ToList();

                        d.Files = Mapper.Map<List<Document>>(files);

                        d.Files?.ForEach(y => y.Path = GetDocumentPath(y));
                    }
                });
            });
        }

        private string GetDocumentPath(File file)
        {
            if (file.Guid.HasValue == false)
                return null;

            return _helperPath
                .GetRelativeFilePath(file.Type, file.Guid.Value, file.Name)
                .Replace("\\", "/");
        }
    }
}