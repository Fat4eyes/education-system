using System;
using EducationSystem.Exceptions.Source.Helpers;
using EducationSystem.Helpers.Interfaces.Source;
using EducationSystem.Models.Source.Rest;
using EducationSystem.Repositories.Interfaces.Source.Rest;

namespace EducationSystem.Helpers.Implementations.Source
{
    public sealed class HelperTheme : IHelperTheme
    {
        private readonly IRepositoryDiscipline _repositoryDiscipline;

        public HelperTheme(IRepositoryDiscipline repositoryDiscipline)
        {
            _repositoryDiscipline = repositoryDiscipline;
        }

        public void ValidateTheme(Theme theme)
        {
            if (theme == null)
                throw new ArgumentNullException(nameof(theme));

            if (string.IsNullOrWhiteSpace(theme.Name))
                throw ExceptionHelper.CreatePublicException("Не указано название темы.");

            if (_repositoryDiscipline.GetById(theme.DisciplineId) == null)
                throw ExceptionHelper.CreatePublicException("Указанная дисциплина не существует.");
        }
    }
}