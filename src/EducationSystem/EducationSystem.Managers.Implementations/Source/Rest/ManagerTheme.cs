using System.Collections.Generic;
using AutoMapper;
using EducationSystem.Constants.Source;
using EducationSystem.Exceptions.Source.Helpers;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Managers.Implementations.Source.Rest
{
    public class ManagerTheme : Manager<ManagerTheme>, IManagerTheme
    {
        protected IRepositoryTheme RepositoryTheme { get; }

        public ManagerTheme(
            IMapper mapper,
            ILogger<ManagerTheme> logger,
            IRepositoryTheme repositoryTheme)
            : base(mapper, logger)
        {
            RepositoryTheme = repositoryTheme;
        }

        public PagedData<Theme> GetThemes(OptionsTheme options)
        {
            var (count, themes) = RepositoryTheme.GetThemes(options);

            return new PagedData<Theme>(Mapper.Map<List<Theme>>(themes), count);
        }

        public PagedData<Theme> GetThemesByTestId(int testId, OptionsTheme options)
        {
            var (count, themes) = RepositoryTheme.GetThemesByTestId(testId, options);

            return new PagedData<Theme>(Mapper.Map<List<Theme>>(themes), count);
        }

        public PagedData<Theme> GetThemesByDisciplineId(int disciplineId, OptionsTheme options)
        {
            var (count, themes) = RepositoryTheme.GetThemesByDisciplineId(disciplineId, options);

            return new PagedData<Theme>(Mapper.Map<List<Theme>>(themes), count);
        }

        public Theme GetThemeById(int id, OptionsTheme options)
        {
            var theme = RepositoryTheme.GetThemeById(id, options) ??
                throw ExceptionHelper.CreateNotFoundException(
                    Messages.Theme.NotFoundById(id),
                    Messages.Theme.NotFoundPublic);

            return Mapper.Map<Theme>(theme);
        }
    }
}