using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Interfaces.Services;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Rest;
using EducationSystem.Specifications.Disciplines;
using EducationSystem.Specifications.Themes;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Services
{
    public sealed class ServiceTheme : Service<ServiceTheme>, IServiceTheme
    {
        private readonly IValidator<Theme> _validatorTheme;
        private readonly IRepository<DatabaseTheme> _repositoryTheme;
        private readonly IRepository<DatabaseDiscipline> _repositoryDiscipline;

        public ServiceTheme(
            IMapper mapper,
            ILogger<ServiceTheme> logger,
            IValidator<Theme> validatorTheme,
            IExceptionFactory exceptionFactory,
            IExecutionContext executionContext,
            IRepository<DatabaseTheme> repositoryTheme,
            IRepository<DatabaseDiscipline> repositoryDiscipline)
            : base(
                mapper,
                logger,
                executionContext,
                exceptionFactory)
        {
            _validatorTheme = validatorTheme;
            _repositoryTheme = repositoryTheme;
            _repositoryDiscipline = repositoryDiscipline;
        }

        public async Task<PagedData<Theme>> GetThemesAsync(FilterTheme filter)
        {
            if (CurrentUser.IsAdmin())
            {
                var specification =
                    new ThemesByTestId(filter.TestId) &
                    new ThemesByDisciplineId(filter.DisciplineId);

                var (count, themes) = await _repositoryTheme.FindPaginatedAsync(specification, filter);

                return new PagedData<Theme>(Mapper.Map<List<Theme>>(themes), count);
            }

            if (CurrentUser.IsLecturer())
            {
                var specification =
                    new ThemesByTestId(filter.TestId) &
                    new ThemesByDisciplineId(filter.DisciplineId) &
                    new ThemesByLecturerId(CurrentUser.Id);

                var (count, themes) = await _repositoryTheme.FindPaginatedAsync(specification, filter);

                return new PagedData<Theme>(Mapper.Map<List<Theme>>(themes), count);
            }

            throw ExceptionFactory.NoAccess();
        }

        public async Task<Theme> GetThemeAsync(int id)
        {
            if (CurrentUser.IsAdmin())
            {
                var theme = await _repositoryTheme.FindFirstAsync(new ThemesById(id)) ??
                    throw ExceptionFactory.NotFound<DatabaseTheme>(id);

                return Mapper.Map<Theme>(theme);
            }

            if (CurrentUser.IsLecturer())
            {
                var theme = await _repositoryTheme.FindFirstAsync(new ThemesById(id)) ??
                    throw ExceptionFactory.NotFound<DatabaseTheme>(id);

                if (new ThemesByLecturerId(CurrentUser.Id).IsSatisfiedBy(theme) == false)
                    throw ExceptionFactory.NoAccess();

                return Mapper.Map<Theme>(theme);
            }

            throw ExceptionFactory.NoAccess();
        }

        public async Task DeleteThemeAsync(int id)
        {
            if (CurrentUser.IsNotAdmin() && CurrentUser.IsNotLecturer())
                throw ExceptionFactory.NoAccess();

            var theme = await _repositoryTheme.FindFirstAsync(new ThemesById(id)) ??
                throw ExceptionFactory.NotFound<DatabaseTheme>(id);

            if (CurrentUser.IsNotAdmin() && !new ThemesByLecturerId(CurrentUser.Id).IsSatisfiedBy(theme))
                throw ExceptionFactory.NoAccess();

            await _repositoryTheme.RemoveAsync(theme, true);
        }

        public async Task<int> CreateThemeAsync(Theme theme)
        {
            if (CurrentUser.IsNotLecturer())
                throw ExceptionFactory.NoAccess();

            await _validatorTheme.ValidateAsync(theme.Format());

            var model = Mapper.Map<DatabaseTheme>(theme);

            model.Order = int.MaxValue;

            await _repositoryTheme.AddAsync(model, true);

            return model.Id;
        }

        public async Task UpdateDisciplineThemesAsync(int id, List<Theme> themes)
        {
            if (CurrentUser.IsNotLecturer())
                throw ExceptionFactory.NoAccess();

            var discipline = await _repositoryDiscipline.FindFirstAsync(new DisciplinesById(id)) ??
                throw ExceptionFactory.NotFound<DatabaseDiscipline>(id);

            if (!new DisciplinesByLecturerId(CurrentUser.Id).IsSatisfiedBy(discipline))
                throw ExceptionFactory.NoAccess();

            var ids = themes.Select(x => x.Id).ToArray();

            var models = await _repositoryTheme.FindAllAsync(new ThemesByIds(ids));

            models.ForEach(x =>
            {
                var theme = themes.FirstOrDefault(y => y.Id == x.Id);

                if (theme?.Order != null && new ThemesByDisciplineId(discipline.Id).IsSatisfiedBy(x))
                    x.Order = theme.Order.Value;
            });

            await _repositoryTheme.UpdateAsync(models, true);
        }

        public async Task UpdateThemeAsync(int id, Theme theme)
        {
            if (CurrentUser.IsNotLecturer())
                throw ExceptionFactory.NoAccess();

            await _validatorTheme.ValidateAsync(theme.Format());

            var model = await _repositoryTheme.FindFirstAsync(new ThemesById(id)) ??
                throw ExceptionFactory.NotFound<DatabaseTheme>(id);

            if (!new ThemesByLecturerId(CurrentUser.Id).IsSatisfiedBy(model))
                throw ExceptionFactory.NoAccess();

            Mapper.Map(Mapper.Map<DatabaseTheme>(theme), model);

            await _repositoryTheme.UpdateAsync(model, true);
        }
    }
}