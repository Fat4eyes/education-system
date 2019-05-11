using System;
using System.Linq;
using System.Threading.Tasks;
using EducationSystem.Database.Models;
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
        private readonly IHelperFile _helperFile;
        private readonly IExecutionContext _executionContext;
        private readonly IRepository<DatabaseFile> _repositoryFile;

        public ValidatorMaterial(
            IHelperFile helperFile,
            IExecutionContext executionContext,
            IRepository<DatabaseFile> repositoryFile)
        {
            _helperFile = helperFile;
            _executionContext = executionContext;
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

            var user = await _executionContext.GetCurrentUserAsync();

            var specification =
                new FilesByIds(ids) &
                new FilesByOwnerId(user.Id);

            if ((await _repositoryFile.FindAllAsync(specification)).Count != ids.Length)
                throw ExceptionHelper.CreatePublicException("Один или несколько указанных файлов не существуют или недоступны.");
        }
    }
}