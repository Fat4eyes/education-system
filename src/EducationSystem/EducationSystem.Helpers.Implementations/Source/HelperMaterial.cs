using System;
using System.Linq;
using EducationSystem.Exceptions.Source.Helpers;
using EducationSystem.Extensions.Source;
using EducationSystem.Helpers.Interfaces.Source;
using EducationSystem.Helpers.Interfaces.Source.Files;
using EducationSystem.Models.Source.Rest;
using EducationSystem.Repositories.Interfaces.Source.Rest;

namespace EducationSystem.Helpers.Implementations.Source
{
    public class HelperMaterial : IHelperMaterial
    {
        private readonly IHelperFileDocument _helperFileDocument;
        private readonly IRepositoryFile _repositoryFile;

        public HelperMaterial(IHelperFileDocument helperFileDocument, IRepositoryFile repositoryFile)
        {
            _helperFileDocument = helperFileDocument;
            _repositoryFile = repositoryFile;
        }

        public void ValidateMaterial(Material material)
        {
            if (material == null)
                throw new ArgumentNullException(nameof(material));

            if (string.IsNullOrWhiteSpace(material.Name))
                throw ExceptionHelper.CreatePublicException("Не указано название материала.");

            if (string.IsNullOrWhiteSpace(material.Template))
                throw ExceptionHelper.CreatePublicException("Не указан шаблон материала.");

            if (material.Files.IsEmpty())
                return;

            if (material.Files.GroupBy(x => x.Id).Any(x => x.Count() > 1))
                throw ExceptionHelper.CreatePublicException("В материале указаны повторяющиеся файлы.");

            if (material.Files.Any(x => _helperFileDocument.FileExists(x.Id) == false))
                throw ExceptionHelper.CreatePublicException("Один или несколько указанных файлов не существуют.");

            if (_repositoryFile.IsFilesExists(material.Files.Select(x => x.Id).ToList()) == false)
                throw ExceptionHelper.CreatePublicException("Один или несколько указанных файлов не существуют.");
        }
    }
}