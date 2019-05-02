using System;
using System.Linq;
using System.Threading.Tasks;
using EducationSystem.Exceptions.Helpers;
using EducationSystem.Extensions;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models.Rest;
using EducationSystem.Repositories.Interfaces;

namespace EducationSystem.Implementations.Validators
{
    public sealed class ValidatorMaterial : IValidator<Material>
    {
        private readonly IHelperFile _helperFile;
        private readonly IRepositoryFile _repositoryFile;

        public ValidatorMaterial(IHelperFile helperFile, IRepositoryFile repositoryFile)
        {
            _helperFile = helperFile;
            _repositoryFile = repositoryFile;
        }

        public async Task ValidateAsync(Material model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (string.IsNullOrWhiteSpace(model.Name))
                throw ExceptionHelper.CreatePublicException("Не указано название материала.");

            if (string.IsNullOrWhiteSpace(model.Template))
                throw ExceptionHelper.CreatePublicException("Не указан шаблон материала.");

            if (model.Files.IsEmpty())
                return;

            if (model.Files.GroupBy(x => x.Id).Any(x => x.Count() > 1))
                throw ExceptionHelper.CreatePublicException("В материале указаны повторяющиеся файлы.");

            if (await model.Files.AllAsync(x => _helperFile.FileExistsAsync(x)) == false)
                throw ExceptionHelper.CreatePublicException("Один или несколько указанных файлов не существуют.");

            var ids = model.Files
                .Select(x => x.Id)
                .ToArray();

            var files = await _repositoryFile.GetByIdsAsync(ids);

            if (files.Count != ids.Length)
                throw ExceptionHelper.CreatePublicException("Один или несколько указанных файлов не существуют.");
        }
    }
}