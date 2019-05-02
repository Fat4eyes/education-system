using System;
using System.Threading.Tasks;
using EducationSystem.Exceptions.Helpers;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models.Rest;

namespace EducationSystem.Implementations.Validators
{
    public sealed class ValidatorTheme : IValidator<Theme>
    {
        private readonly IRepositoryDiscipline _repositoryDiscipline;

        public ValidatorTheme(IRepositoryDiscipline repositoryDiscipline)
        {
            _repositoryDiscipline = repositoryDiscipline;
        }

        public async Task ValidateAsync(Theme model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (string.IsNullOrWhiteSpace(model.Name))
                throw ExceptionHelper.CreatePublicException("Не указано название темы.");

            if (await _repositoryDiscipline.GetByIdAsync(model.DisciplineId) == null)
                throw ExceptionHelper.CreatePublicException("Указанная дисциплина не существует.");
        }
    }
}