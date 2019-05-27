using System;
using System.Linq;
using System.Threading.Tasks;
using EducationSystem.Database.Models;
using EducationSystem.Enums;
using EducationSystem.Extensions;
using EducationSystem.Helpers;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models.Rest;
using EducationSystem.Specifications.Files;

namespace EducationSystem.Implementations.Validators
{
    public sealed class ValidatorMaterial : IValidator<Material>
    {
        private readonly IContext _context;
        private readonly IHelperFile _helperFile;
        private readonly IRepository<DatabaseFile> _repositoryFile;

        public ValidatorMaterial(
            IContext context,
            IHelperFile helperFile,
            IRepository<DatabaseFile> repositoryFile)
        {
            _context = context;
            _helperFile = helperFile;
            _repositoryFile = repositoryFile;
        }

        public async Task ValidateAsync(Material model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (string.IsNullOrWhiteSpace(model.Name))
                throw ExceptionHelper.CreatePublicException("Не указано название материала.");

            if (model.Name.Length > 255)
                throw ExceptionHelper.CreatePublicException("Название материала не может превышать 255 символов.");

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

            var user = await _context.GetCurrentUserAsync();

            var specification =
                new FilesByIds(ids) &
                new FilesByOwnerId(user.Id);

            var files = await _repositoryFile.FindAllAsync(specification);

            if (files.Any(x => x.Type != FileType.Document))
                throw ExceptionHelper.CreatePublicException("Один или несколько указанных файлов имеют неверный тип.");

            if (files.Count != ids.Length)
                throw ExceptionHelper.CreatePublicException("Один или несколько указанных файлов не существуют или недоступны.");

            if (model.Anchors.IsEmpty())
                return;

            if (model.Anchors.Any(x => string.IsNullOrWhiteSpace(x.Token) || string.IsNullOrWhiteSpace(x.Name)))
                throw ExceptionHelper.CreatePublicException("Один или несколько указанных якорей не заполнены.");

            if (model.Anchors.Any(x =>  x.Token.Length > 255 || x.Name.Length > 255))
                throw ExceptionHelper.CreatePublicException(
                    "Один или несколько указанных якорей заполнены некорректно. " +
                    "Максимальная длина токена: 255 символов. " +
                    "Максимальная длина названия: 255 символов.");

            if (model.Anchors.GroupBy(x => x.Token).Any(x => x.Count() > 1))
                throw ExceptionHelper.CreatePublicException("В материале указаны повторяющиеся якоря.");
        }
    }
}