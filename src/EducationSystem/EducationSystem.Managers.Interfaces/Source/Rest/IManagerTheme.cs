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

        void DeleteThemeById(int id);

        Theme CreateTheme(Theme theme);
        Theme UpdateTheme(int id, Theme theme);
    }
}