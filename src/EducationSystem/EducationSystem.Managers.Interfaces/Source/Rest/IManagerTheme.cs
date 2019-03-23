using System.Threading.Tasks;
using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;

namespace EducationSystem.Managers.Interfaces.Source.Rest
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
    }
}