using System;
using System.Threading.Tasks;
using EducationSystem.Database.Models;
using EducationSystem.Exceptions.Helpers;
using EducationSystem.Implementations.Specifications;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models.Rest;

namespace EducationSystem.Implementations.Validators
{
    public sealed class ValidatorTheme : IValidator<Theme>
    {
        private readonly IExecutionContext _executionContext;
        private readonly IRepository<DatabaseDiscipline> _repositoryDiscipline;

        public ValidatorTheme(
            IExecutionContext executionContext,
            IRepository<DatabaseDiscipline> repositoryDiscipline)
        {
            _executionContext = executionContext;
            _repositoryDiscipline = repositoryDiscipline;
        }

        public async Task ValidateAsync(Theme model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (string.IsNullOrWhiteSpace(model.Name))
                throw ExceptionHelper.CreatePublicException("Не указано название темы.");

            var discipline = await _repositoryDiscipline.FindFirstAsync(new DisciplinesById(model.DisciplineId)) ??
                throw ExceptionHelper.CreatePublicException("Указанная дисциплина не существует.");

            var user = await _executionContext.GetCurrentUserAsync();

            if (new DisciplinesByLecturerId(user.Id).IsSatisfiedBy(discipline) == false)
                throw ExceptionHelper.CreatePublicException("Указанная дисциплина недоступна.");
        }
    }
}