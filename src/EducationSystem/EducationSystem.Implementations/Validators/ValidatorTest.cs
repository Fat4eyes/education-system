using System;
using System.Linq;
using System.Threading.Tasks;
using EducationSystem.Exceptions.Helpers;
using EducationSystem.Extensions;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models.Rest;

namespace EducationSystem.Implementations.Validators
{
    public sealed class ValidatorTest : IValidator<Test>
    {
        private readonly IRepositoryTheme _repositoryTheme;
        private readonly IRepositoryDiscipline _repositoryDiscipline;

        public ValidatorTest(IRepositoryTheme repositoryTheme, IRepositoryDiscipline repositoryDiscipline)
        {
            _repositoryTheme = repositoryTheme;
            _repositoryDiscipline = repositoryDiscipline;
        }

        public async Task ValidateAsync(Test model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (string.IsNullOrWhiteSpace(model.Subject))
                throw ExceptionHelper.CreatePublicException("Не указано название теста.");

            if (model.TotalTime.HasValue == false)
                throw ExceptionHelper.CreatePublicException("Не указано общее время теста.");

            if (model.TotalTime.Value <= 0)
                throw ExceptionHelper.CreatePublicException("Указано некорректное общее время теста.");

            if (model.Attempts.HasValue == false)
                throw ExceptionHelper.CreatePublicException("Не указано количество попыток прохождения теста.");

            if (model.Type.HasValue == false)
                throw ExceptionHelper.CreatePublicException("Не указан тип теста.");

            if (model.Themes.IsEmpty())
                throw ExceptionHelper.CreatePublicException("В тесте не указана ни одна тема.");

            if (model.Themes.GroupBy(x => x.Id).Any(x => x.Count() > 1))
                throw ExceptionHelper.CreatePublicException("В тесте указаны повторяющиеся темы.");

            if (await _repositoryDiscipline.GetByIdAsync(model.DisciplineId) == null)
                throw ExceptionHelper.CreatePublicException("Указанная дисциплина не существует.");

            var ids = model.Themes
                .Select(x => x.Id)
                .ToArray();

            var themes = await _repositoryTheme.GetByIdsAsync(ids);

            if (themes.Count != ids.Length)
                throw ExceptionHelper.CreatePublicException("Одна или несколько выбранных тем не существуют.");
        }
    }
}