using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Database.Models.Source;
using EducationSystem.Exceptions.Source.Helpers;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Managers.Rest;
using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;
using EducationSystem.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Managers.Rest
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

        public async Task DeleteThemeByIdAsync(int id)
        {
            var theme = _repositoryTheme.GetById(id) ??
                throw ExceptionHelper.CreateNotFoundException(
                    $"Тема для удаления не найдена. Идентификатор темы: {id}.",
                    $"Тема для удаления не найдена.");

            await _repositoryTheme.RemoveAsync(theme, true);
        }

        public async Task<Theme> CreateThemeAsync(Theme theme)
        {
            _helperTheme.ValidateTheme(theme);

            FormatTheme(theme);

            var model = Mapper.Map<DatabaseTheme>(theme);

            await _repositoryTheme.AddAsync(model, true);

            return Mapper.Map<DatabaseTheme, Theme>(model);
        }

        public async Task<Theme> UpdateThemeAsync(int id, Theme theme)
        {
            _helperTheme.ValidateTheme(theme);

            var model = _repositoryTheme.GetById(id) ??
                throw ExceptionHelper.CreateNotFoundException(
                    $"Тема для обновления не найдена. Идентификатор темы: {id}.",
                    $"Тема для обновления не найдена.");

            FormatTheme(theme);

            Mapper.Map(Mapper.Map<DatabaseTheme>(theme), model);

            await _repositoryTheme.UpdateAsync(model, true);

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

        private static void FormatTheme(Theme theme)
        {
            theme.Name = theme.Name.Trim();
        }
    }
}