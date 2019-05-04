using System.Collections.Generic;
using System.Threading.Tasks;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Interfaces.Managers;
using EducationSystem.Interfaces.Services;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Rest;

namespace EducationSystem.Implementations.Managers
{
    public sealed class ManagerTheme : Manager, IManagerTheme
    {
        private readonly IServiceTheme _serviceTheme;

        public ManagerTheme(
            IExecutionContext executionContext,
            IExceptionFactory exceptionFactory,
            IServiceTheme serviceTheme)
            : base(
                executionContext,
                exceptionFactory)
        {
            _serviceTheme = serviceTheme;
        }

        public async Task<PagedData<Theme>> GetThemesAsync(FilterTheme filter)
        {
            if (CurrentUser.IsAdmin())
                return await _serviceTheme.GetThemesAsync(filter);

            if (CurrentUser.IsLecturer())
                return await _serviceTheme.GetLecturerThemesAsync(CurrentUser.Id, filter);

            throw ExceptionFactory.NoAccess();
        }

        public async Task<Theme> GetThemeAsync(int id)
        {
            if (CurrentUser.IsAdmin())
                return await _serviceTheme.GetThemeAsync(id);

            if (CurrentUser.IsLecturer())
                return await _serviceTheme.GetLecturerThemeAsync(id, CurrentUser.Id);

            throw ExceptionFactory.NoAccess();
        }

        public async Task DeleteThemeAsync(int id)
        {
            if (CurrentUser.IsNotAdmin() && CurrentUser.IsNotLecturer())
                throw ExceptionFactory.NoAccess();

            await _serviceTheme.DeleteThemeAsync(id);
        }

        public async Task UpdateThemeAsync(int id, Theme theme)
        {
            if (CurrentUser.IsNotLecturer())
                throw ExceptionFactory.NoAccess();

            await _serviceTheme.UpdateThemeAsync(id, theme);
        }

        public async Task<int> CreateThemeAsync(Theme theme)
        {
            if (CurrentUser.IsLecturer())
                return await _serviceTheme.CreateThemeAsync(theme);

            throw ExceptionFactory.NoAccess();
        }

        public async Task UpdateDisciplineThemesAsync(int id, List<Theme> themes)
        {
            if (CurrentUser.IsNotLecturer())
                throw ExceptionFactory.NoAccess();

            await _serviceTheme.UpdateDisciplineThemesAsync(id, themes);
        }
    }
}