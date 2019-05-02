using System.Threading.Tasks;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.Models.Rest;

namespace EducationSystem.Interfaces.Services
{
    public interface IServiceTheme
    {
        Task<PagedData<Theme>> GetThemesAsync(OptionsTheme options, FilterTheme filter);
        Task<PagedData<Theme>> GetLecturerThemesAsync(int lecturerId, OptionsTheme options, FilterTheme filter);

        Task<Theme> GetThemeAsync(int id, OptionsTheme options);
        Task<Theme> GetLecturerThemeAsync(int id, int lecturerId, OptionsTheme options);

        Task DeleteThemeAsync(int id);
        Task UpdateThemeAsync(int id, Theme theme);
        Task<int> CreateThemeAsync(Theme theme);
    }
}