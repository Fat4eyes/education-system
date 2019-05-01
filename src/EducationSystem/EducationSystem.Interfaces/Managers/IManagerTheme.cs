using System.Collections.Generic;
using System.Threading.Tasks;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.Models.Rest;

namespace EducationSystem.Interfaces.Managers
{
    public interface IManagerTheme
    {
        Task<PagedData<Theme>> GetThemesByTestIdAsync(int testId, OptionsTheme options, FilterTheme filter);
        Task<PagedData<Theme>> GetThemesByDisciplineIdAsync(int disciplineId, OptionsTheme options, FilterTheme filter);

        Task DeleteThemeAsync(int id);
        Task<Theme> GetThemeAsync(int id, OptionsTheme options);
        Task<Theme> CreateThemeAsync(Theme theme);
        Task<Theme> UpdateThemeAsync(int id, Theme theme);

        Task UpdateDisciplineThemesAsync(int disciplineId, List<Theme> themes);
    }
}