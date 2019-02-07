using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;

namespace EducationSystem.Managers.Interfaces.Source.Rest
{
    public interface IManagerTheme
    {
        PagedData<Theme> GetThemes(OptionsTheme options);

        PagedData<Theme> GetThemesByTestId(int testId, OptionsTheme options);

        PagedData<Theme> GetThemesByDisciplineId(int disciplineId, OptionsTheme options);

        Theme GetThemeById(int id, OptionsTheme options);
    }
}