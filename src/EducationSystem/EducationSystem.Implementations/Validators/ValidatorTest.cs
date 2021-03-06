﻿using System;
using System.Linq;
using System.Threading.Tasks;
using EducationSystem.Database.Models;
using EducationSystem.Extensions;
using EducationSystem.Helpers;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models.Rest;
using EducationSystem.Specifications.Disciplines;
using EducationSystem.Specifications.Themes;

namespace EducationSystem.Implementations.Validators
{
    public sealed class ValidatorTest : IValidator<Test>
    {
        private readonly IContext _context;
        private readonly IRepository<DatabaseTheme> _repositoryTheme;
        private readonly IRepository<DatabaseDiscipline> _repositoryDiscipline;

        public ValidatorTest(
            IContext context,
            IRepository<DatabaseTheme> repositoryTheme,
            IRepository<DatabaseDiscipline> repositoryDiscipline)
        {
            _context = context;
            _repositoryTheme = repositoryTheme;
            _repositoryDiscipline = repositoryDiscipline;
        }

        public async Task ValidateAsync(Test model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (string.IsNullOrWhiteSpace(model.Subject))
                throw ExceptionHelper.CreatePublicException("Не указано название теста.");

            if (model.Subject.Length > 200)
                throw ExceptionHelper.CreatePublicException("Название теста не может превышать 200 символов.");

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

            var discipline = await _repositoryDiscipline.FindFirstAsync(new DisciplinesById(model.DisciplineId)) ??
                throw ExceptionHelper.CreatePublicException("Указанная дисциплина не существует.");

            var user = await _context.GetCurrentUserAsync();

            if (new DisciplinesByLecturerId(user.Id).IsSatisfiedBy(discipline) == false)
                throw ExceptionHelper.CreatePublicException("Указанная дисциплина недоступна.");

            var ids = model.Themes
                .Select(x => x.Id)
                .ToArray();

            var specification =
                new ThemesByIds(ids) &
                new ThemesByLecturerId(user.Id);

            if (await _repositoryTheme.GetCountAsync(specification) != ids.Length)
                throw ExceptionHelper.CreatePublicException("Одна или несколько выбранных тем не существуют или недоступны.");
        }
    }
}