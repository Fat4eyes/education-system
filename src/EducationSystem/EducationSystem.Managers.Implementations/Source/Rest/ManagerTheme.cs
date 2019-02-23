using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EducationSystem.Constants.Source;
using EducationSystem.Database.Models.Source;
using EducationSystem.Exceptions.Source.Helpers;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Filters;
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

        public PagedData<Theme> GetThemes(OptionsTheme options, FilterTheme filter)
        {
            var (count, themes) = RepositoryTheme.GetThemes(filter);

            return new PagedData<Theme>(themes.Select(x => Map(x, options)).ToList(), count);
        }

        public PagedData<Theme> GetThemesByTestId(int testId, OptionsTheme options, FilterTheme filter)
        {
            var (count, themes) = RepositoryTheme.GetThemesByTestId(testId, filter);

            return new PagedData<Theme>(themes.Select(x => Map(x, options)).ToList(), count);
        }

        public PagedData<Theme> GetThemesByDisciplineId(int disciplineId, OptionsTheme options, FilterTheme filter)
        {
            var (count, themes) = RepositoryTheme.GetThemesByDisciplineId(disciplineId, filter);

            return new PagedData<Theme>(themes.Select(x => Map(x, options)).ToList(), count);
        }

        public Theme GetThemeById(int id, OptionsTheme options)
        {
            var theme = RepositoryTheme.GetById(id) ??
                throw ExceptionHelper.CreateNotFoundException(
                    Messages.Theme.NotFoundById(id),
                    Messages.Theme.NotFoundPublic);

            return Mapper.Map<Theme>(Map(theme, options));
        }

        public void DeleteThemeById(int id)
        {
            var theme = RepositoryTheme.GetById(id) ??
                throw ExceptionHelper.CreateNotFoundException(
                    Messages.Theme.NotFoundById(id),
                    Messages.Theme.NotFoundPublic);

            RepositoryTheme.Delete(theme);
            RepositoryTheme.SaveChanges();
        }

        private Theme Map(DatabaseTheme theme, OptionsTheme options)
        {
            return Mapper.Map<DatabaseTheme, Theme>(theme, x =>
            {
                x.AfterMap((s, d) =>
                {
                    if (options.WithQuestions)
                        d.Questions = Mapper.Map<List<Question>>(s.Questions);
                });
            });
        }
    }
}