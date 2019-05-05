using System.Collections.Generic;
using System.Threading.Tasks;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Rest;

namespace EducationSystem.Interfaces.Services
{
    public interface IServiceTheme
    {
        Task<PagedData<Theme>> GetThemesAsync(FilterTheme filter);

        Task<Theme> GetThemeAsync(int id);

        Task DeleteThemeAsync(int id);
        Task UpdateThemeAsync(int id, Theme theme);
        Task<int> CreateThemeAsync(Theme theme);

        Task UpdateDisciplineThemesAsync(int id, List<Theme> themes);
    }
}