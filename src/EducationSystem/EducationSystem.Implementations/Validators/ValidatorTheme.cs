using System;
using System.Threading.Tasks;
using EducationSystem.Database.Models;
using EducationSystem.Exceptions.Helpers;
using EducationSystem.Implementations.Specifications;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models.Rest;

namespace EducationSystem.Implementations.Validators
{
    public sealed class ValidatorTheme : IValidator<Theme>
    {
        private readonly IRepository<DatabaseDiscipline> _repositoryDiscipline;

        public ValidatorTheme(IRepository<DatabaseDiscipline> repositoryDiscipline)
        {
            _repositoryDiscipline = repositoryDiscipline;
        }

        public async Task ValidateAsync(Theme model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (string.IsNullOrWhiteSpace(model.Name))
                throw ExceptionHelper.CreatePublicException("Не указано название темы.");

            if (await _repositoryDiscipline.FindFirstAsync(new DisciplinesById(model.DisciplineId)) == null)
                throw ExceptionHelper.CreatePublicException("Указанная дисциплина не существует.");
        }
    }
}