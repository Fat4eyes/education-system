﻿using System;
using EducationSystem.Exceptions.Helpers;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models.Source.Rest;
using EducationSystem.Repositories.Interfaces;

namespace EducationSystem.Implementations.Validators
{
    public sealed class ValidatorTheme : IValidator<Theme>
    {
        private readonly IRepositoryDiscipline _repositoryDiscipline;

        public ValidatorTheme(IRepositoryDiscipline repositoryDiscipline)
        {
            _repositoryDiscipline = repositoryDiscipline;
        }

        public void Validate(Theme model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (string.IsNullOrWhiteSpace(model.Name))
                throw ExceptionHelper.CreatePublicException("Не указано название темы.");

            if (_repositoryDiscipline.GetById(model.DisciplineId) == null)
                throw ExceptionHelper.CreatePublicException("Указанная дисциплина не существует.");
        }
    }
}