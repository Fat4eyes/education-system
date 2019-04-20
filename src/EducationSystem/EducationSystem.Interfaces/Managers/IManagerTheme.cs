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
        PagedData<Theme> GetThemes(OptionsTheme options, FilterTheme filter);
        PagedData<Theme> GetThemesByTestId(int testId, OptionsTheme options, FilterTheme filter);
        PagedData<Theme> GetThemesByDisciplineId(int disciplineId, OptionsTheme options, FilterTheme filter);

        Theme GetThemeById(int id, OptionsTheme options);

        Task DeleteThemeByIdAsync(int id);

        Task<Theme> CreateThemeAsync(Theme theme);

        Task<Theme> UpdateThemeAsync(int id, Theme theme);

        Task UpdateDisciplineThemesAsync(int disciplineId, List<Theme> themes);
    }
}