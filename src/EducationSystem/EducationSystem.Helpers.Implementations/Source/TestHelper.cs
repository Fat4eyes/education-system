using System;
using System.Linq;
using EducationSystem.Exceptions.Source.Helpers;
using EducationSystem.Extensions.Source;
using EducationSystem.Helpers.Interfaces.Source;
using EducationSystem.Models.Source.Rest;
using EducationSystem.Repositories.Interfaces.Source.Rest;

namespace EducationSystem.Helpers.Implementations.Source
{
    public class TestHelper : ITestHelper
    {
        private readonly IRepositoryTheme _repositoryTheme;

        public TestHelper(IRepositoryTheme repositoryTheme)
        {
            _repositoryTheme = repositoryTheme;
        }

        public void ValidateTest(Test test)
        {
            if (test == null)
                throw new ArgumentNullException(nameof(test));

            if (string.IsNullOrWhiteSpace(test.Subject))
                throw ExceptionHelper.CreatePublicException("Не указано название теста.");

            if (test.TotalTime.HasValue == false)
                throw ExceptionHelper.CreatePublicException("Не указано общее время теста.");

            if (test.Attempts.HasValue == false)
                throw ExceptionHelper.CreatePublicException("Не указано количество попыток прохождения теста.");

            if (test.Type.HasValue == false)
                throw ExceptionHelper.CreatePublicException("Не указан тип теста.");

            if (test.Themes.IsEmpty())
                throw ExceptionHelper.CreatePublicException("Тест не содержит темы.");

            if (_repositoryTheme.CheckThemesExistence(test.Themes.Select(x => x.Id).ToList()) == false)
                throw ExceptionHelper.CreatePublicException("Одна или несколько выбранных тем не существуют.");
        }
    }
}