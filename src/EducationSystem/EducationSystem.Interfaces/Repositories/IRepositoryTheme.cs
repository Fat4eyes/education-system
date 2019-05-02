using System.Collections.Generic;
using System.Threading.Tasks;
using EducationSystem.Database.Models;
using EducationSystem.Interfaces.Repositories.Basics;
using EducationSystem.Models.Filters;

namespace EducationSystem.Interfaces.Repositories
{
    public interface IRepositoryTheme : IRepository<DatabaseTheme>
    {
        Task<(int Count, List<DatabaseTheme> Themes)> GetThemesAsync(FilterTheme filter);
        Task<(int Count, List<DatabaseTheme> Themes)> GetLecturerThemesAsync(int lecturerId, FilterTheme filter);

        Task<DatabaseTheme> GetLecturerThemeAsync(int id, int lecturerId);
    }
}