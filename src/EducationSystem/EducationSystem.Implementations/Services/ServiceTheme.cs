using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Implementations.Specifications;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Interfaces.Services;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Rest;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Services
{
    public sealed class ServiceTheme : Service<ServiceTheme>, IServiceTheme
    {
        private readonly IHelperUserRole _helperUserRole;
        private readonly IValidator<Theme> _validatorTheme;
        private readonly IExceptionFactory _exceptionFactory;
        private readonly IExecutionContext _executionContext;
        private readonly IRepository<DatabaseTheme> _repositoryTheme;

        public ServiceTheme(
            IMapper mapper,
            ILogger<ServiceTheme> logger,
            IHelperUserRole helperUserRole,
            IValidator<Theme> validatorTheme,
            IExceptionFactory exceptionFactory,
            IExecutionContext executionContext,
            IRepository<DatabaseTheme> repositoryTheme)
            : base(mapper, logger)
        {
            _helperUserRole = helperUserRole;
            _validatorTheme = validatorTheme;
            _exceptionFactory = exceptionFactory;
            _executionContext = executionContext;
            _repositoryTheme = repositoryTheme;
        }

        public async Task<PagedData<Theme>> GetThemesAsync(FilterTheme filter)
        {
            var specification =
                new ThemesByTestId(filter.TestId) &
                new ThemesByDisciplineId(filter.DisciplineId);

            var (count, themes) = await _repositoryTheme.FindPaginatedAsync(specification, filter);

            return new PagedData<Theme>(Mapper.Map<List<Theme>>(themes), count);
        }

        public async Task<PagedData<Theme>> GetLecturerThemesAsync(int lecturerId, FilterTheme filter)
        {
            await _helperUserRole.CheckRoleLecturerAsync(lecturerId);

            var specification =
                new ThemesByTestId(filter.TestId) &
                new ThemesByDisciplineId(filter.DisciplineId) &
                new ThemesByLecturerId(lecturerId);

            var (count, themes) = await _repositoryTheme.FindPaginatedAsync(specification, filter);

            return new PagedData<Theme>(Mapper.Map<List<Theme>>(themes), count);
        }

        public async Task<Theme> GetThemeAsync(int id)
        {
            var theme = await _repositoryTheme.FindFirstAsync(new ThemesById(id)) ??
                throw _exceptionFactory.NotFound<DatabaseTheme>(id);

            return Mapper.Map<Theme>(theme);
        }

        public async Task<Theme> GetLecturerThemeAsync(int id, int lecturerId)
        {
            await _helperUserRole.CheckRoleLecturerAsync(lecturerId);

            var specification =
                new ThemesById(id) &
                new ThemesByLecturerId(lecturerId);

            var theme = await _repositoryTheme.FindFirstAsync(specification) ??
                throw _exceptionFactory.NotFound<DatabaseTheme>(id);

            return Mapper.Map<Theme>(theme);
        }

        public async Task DeleteThemeAsync(int id)
        {
            var theme = await _repositoryTheme.FindFirstAsync(new ThemesById(id)) ??
                throw _exceptionFactory.NotFound<DatabaseTheme>(id);

            var user = await _executionContext.GetCurrentUserAsync();

            if (user.IsNotAdmin() && !new ThemesByLecturerId(user.Id).IsSatisfiedBy(theme))
                throw _exceptionFactory.NoAccess();

            await _repositoryTheme.RemoveAsync(theme, true);
        }

        public async Task<int> CreateThemeAsync(Theme theme)
        {
            await _validatorTheme.ValidateAsync(theme.Format());

            var model = Mapper.Map<DatabaseTheme>(theme);

            model.Order = int.MaxValue;

            await _repositoryTheme.AddAsync(model, true);

            return model.Id;
        }

        public async Task UpdateThemeAsync(int id, Theme theme)
        {
            await _validatorTheme.ValidateAsync(theme.Format());

            var model = await _repositoryTheme.FindFirstAsync(new ThemesById(id)) ??
                throw _exceptionFactory.NotFound<DatabaseTheme>(id);

            var user = await _executionContext.GetCurrentUserAsync();

            if (!new ThemesByLecturerId(user.Id).IsSatisfiedBy(model))
                throw _exceptionFactory.NoAccess();

            Mapper.Map(Mapper.Map<DatabaseTheme>(theme), model);

            await _repositoryTheme.UpdateAsync(model, true);
        }
    }
}