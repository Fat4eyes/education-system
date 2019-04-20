using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Exceptions.Helpers;
using EducationSystem.Extensions;
using EducationSystem.Interfaces.Managers;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.Models.Rest;
using EducationSystem.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Managers
{
    public sealed class ManagerTheme : Manager<ManagerTheme>, IManagerTheme
    {
        private readonly IValidator<Theme> _validatorTheme;
        private readonly IRepositoryTheme _repositoryTheme;
        private readonly IRepositoryDiscipline _repositoryDiscipline;

        public ManagerTheme(
            IMapper mapper,
            ILogger<ManagerTheme> logger,
            IValidator<Theme> validatorTheme,
            IRepositoryTheme repositoryTheme,
            IRepositoryDiscipline repositoryDiscipline)
            : base(mapper, logger)
        {
            _validatorTheme = validatorTheme;
            _repositoryTheme = repositoryTheme;
            _repositoryDiscipline = repositoryDiscipline;
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
            _validatorTheme.Validate(theme);

            FormatTheme(theme);

            var model = Mapper.Map<DatabaseTheme>(theme);

            await _repositoryTheme.AddAsync(model, true);

            return Mapper.Map<DatabaseTheme, Theme>(model);
        }

        public async Task<Theme> UpdateThemeAsync(int id, Theme theme)
        {
            _validatorTheme.Validate(theme);

            var model = _repositoryTheme.GetById(id) ??
                throw ExceptionHelper.CreateNotFoundException(
                    $"Тема для обновления не найдена. Идентификатор темы: {id}.",
                    $"Тема для обновления не найдена.");

            FormatTheme(theme);

            Mapper.Map(Mapper.Map<DatabaseTheme>(theme), model);

            await _repositoryTheme.UpdateAsync(model, true);

            return Mapper.Map<DatabaseTheme, Theme>(model);
        }

        public async Task UpdateDisciplineThemesAsync(int disciplineId, List<Theme> themes)
        {
            if (themes.IsEmpty())
                throw ExceptionHelper.CreatePublicException("Не указаны темы для обновления.");

            if (themes.GroupBy(x => x.Id).Any(x => x.Count() > 1))
                throw ExceptionHelper.CreatePublicException("Указаны повторяющиеся темы.");

            if (_repositoryTheme.IsThemesExists(themes.Select(x => x.Id).ToList()) == false)
                throw ExceptionHelper.CreatePublicException("Одна или несколько указанных тем не существуют.");

            var discipline = _repositoryDiscipline.GetById(disciplineId) ??
                throw ExceptionHelper.CreateNotFoundException(
                    $"Дисциплина не найдена. Идентификатор дисциплины: {disciplineId}.",
                    $"Дисциплина не найдена.");

            if (discipline.Themes.Count != themes.Count)
                throw ExceptionHelper.CreatePublicException("Указаны не все темы по дисциплине.");

            if (discipline.Themes.All(x => themes.Select(y => y.Id).Contains(x.Id)) == false)
                throw ExceptionHelper.CreatePublicException("У одной или нескольких тем указанная дисциплина не совпадает.");

            var models = _repositoryTheme.GetByIds(themes.Select(x => x.Id).ToArray());

            var order = 1;

            themes.ForEach(x =>
            {
                var model = models.FirstOrDefault(y => y.Id == x.Id) ??
                    throw ExceptionHelper.CreateNotFoundException(
                        $"Тема не найдена. Идентификатор темы: {x.Id}.",
                        $"Тема не найдена.");

                model.Order = order++;
            });

            await _repositoryTheme.UpdateAsync(models, true);
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