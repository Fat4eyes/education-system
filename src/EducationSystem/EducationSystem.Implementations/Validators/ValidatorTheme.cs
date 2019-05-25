using System;
using System.Threading.Tasks;
using EducationSystem.Database.Models;
using EducationSystem.Helpers;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models.Rest;
using EducationSystem.Specifications.Disciplines;

namespace EducationSystem.Implementations.Validators
{
    public sealed class ValidatorTheme : IValidator<Theme>
    {
        private readonly IContext _context;
        private readonly IRepository<DatabaseDiscipline> _repositoryDiscipline;

        public ValidatorTheme(IContext context, IRepository<DatabaseDiscipline> repositoryDiscipline)
        {
            _context = context;
            _repositoryDiscipline = repositoryDiscipline;
        }

        public async Task ValidateAsync(Theme model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (string.IsNullOrWhiteSpace(model.Name))
                throw ExceptionHelper.CreatePublicException("Не указано название темы.");

            if (model.Name.Length > 200)
                throw ExceptionHelper.CreatePublicException("Название темы не может превышать 200 символов.");

            var discipline = await _repositoryDiscipline.FindFirstAsync(new DisciplinesById(model.DisciplineId)) ??
                throw ExceptionHelper.CreatePublicException("Указанная дисциплина не существует.");

            var user = await _context.GetCurrentUserAsync();

            if (new DisciplinesByLecturerId(user.Id).IsSatisfiedBy(discipline) == false)
                throw ExceptionHelper.CreatePublicException("Указанная дисциплина недоступна.");
        }
    }
}