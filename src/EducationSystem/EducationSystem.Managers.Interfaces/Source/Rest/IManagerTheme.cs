using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;

namespace EducationSystem.Managers.Interfaces.Source.Rest
{
    public interface IManagerTheme
    {
        PagedData<Theme> GetThemesByTestId(int testId, OptionsTheme options);
    }
}