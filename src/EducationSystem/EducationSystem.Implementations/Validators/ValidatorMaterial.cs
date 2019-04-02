using System;
using System.Linq;
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

        public void Validate(Material model)
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

            if (model.Files.Any(x => _helperFile.FileExists(x.Id) == false))
                throw ExceptionHelper.CreatePublicException("Один или несколько указанных файлов не существуют.");

            if (_repositoryFile.IsFilesExists(model.Files.Select(x => x.Id).ToList()) == false)
                throw ExceptionHelper.CreatePublicException("Один или несколько указанных файлов не существуют.");
        }
    }
}