using System.Threading.Tasks;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.Models.Rest;

namespace EducationSystem.Interfaces.Managers
{
    public interface IManagerTheme
    {
        Task<PagedData<Theme>> GetThemesAsync(OptionsTheme options, FilterTheme filter);

        Task<Theme> GetThemeAsync(int id, OptionsTheme options);

        Task DeleteThemeAsync(int id);
        Task UpdateThemeAsync(int id, Theme theme);
        Task<int> CreateThemeAsync(Theme theme);
    }
}