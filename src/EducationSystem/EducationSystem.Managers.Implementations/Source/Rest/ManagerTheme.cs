using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EducationSystem.Database.Models.Source;
using EducationSystem.Exceptions.Source.Helpers;
using EducationSystem.Helpers.Interfaces.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Managers.Implementations.Source.Rest
{
    public sealed class ManagerTheme : Manager<ManagerTheme>, IManagerTheme
    {
        private readonly IHelperTheme _helperTheme;
        private readonly IRepositoryTheme _repositoryTheme;

        public ManagerTheme(
            IMapper mapper,
            ILogger<ManagerTheme> logger,
            IHelperTheme helperTheme,
            IRepositoryTheme repositoryTheme)
            : base(mapper, logger)
        {
            _helperTheme = helperTheme;
            _repositoryTheme = repositoryTheme;
        }

        public PagedData<Theme> GetThemes(OptionsTheme options, FilterTheme filter)
        {
            var (count, themes) = _repositoryTheme.GetThemes(filter);

            return new PagedData<Theme>(themes.Select(x => Map(x, options)).ToList(), count);
        }

        public PagedData<Theme> GetThemesByTestId(int testId, OptionsTheme options, FilterTheme filter)
        {
            var (count, themes) = _repositoryTheme.GetThemesByTestId(testId, filter);

            return new PagedData<Theme>(themes.Select(x => Map(x, options)).ToList(), count);
        }

        public PagedData<Theme> GetThemesByDisciplineId(int disciplineId, OptionsTheme options, FilterTheme filter)
        {
            var (count, themes) = _repositoryTheme.GetThemesByDisciplineId(disciplineId, filter);

            return new PagedData<Theme>(themes.Select(x => Map(x, options)).ToList(), count);
        }

        public Theme GetThemeById(int id, OptionsTheme options)
        {
            var theme = _repositoryTheme.GetById(id) ??
                throw ExceptionHelper.CreateNotFoundException(
                    $"Тема не найдена. Идентификатор темы: {id}.",
                    $"Тема не найдена.");

            return Map(theme, options);
        }

        public void DeleteThemeById(int id)
        {
            _repositoryTheme.Remove(id);
            _repositoryTheme.SaveChanges();
        }

        public Theme CreateTheme(Theme theme)
        {
            _helperTheme.ValidateTheme(theme);

            var model = Mapper.Map<DatabaseTheme>(theme);

            _repositoryTheme.Add(model);
            _repositoryTheme.SaveChanges();

            return Mapper.Map<DatabaseTheme, Theme>(model);
        }

        public Theme UpdateTheme(int id, Theme theme)
        {
            _helperTheme.ValidateTheme(theme);

            var model = _repositoryTheme.GetById(id) ??
                throw ExceptionHelper.CreateNotFoundException(
                    $"Тема для обновления не найдена. Идентификатор темы: {id}.",
                    $"Тема для обновления не найдена.");

            Mapper.Map(Mapper.Map<DatabaseTheme>(theme), model);

            _repositoryTheme.Update(model);
            _repositoryTheme.SaveChanges();

            return Mapper.Map<DatabaseTheme, Theme>(model);
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