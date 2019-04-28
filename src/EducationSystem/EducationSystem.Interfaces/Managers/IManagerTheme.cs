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
        Task<PagedData<Theme>> GetThemesByTestId(int testId, OptionsTheme options, FilterTheme filter);
        Task<PagedData<Theme>> GetThemesByDisciplineId(int disciplineId, OptionsTheme options, FilterTheme filter);

        Task DeleteTheme(int id);
        Task<Theme> GetTheme(int id, OptionsTheme options);
        Task<Theme> CreateTheme(Theme theme);
        Task<Theme> UpdateTheme(int id, Theme theme);

        Task UpdateDisciplineThemes(int disciplineId, List<Theme> themes);
    }
}