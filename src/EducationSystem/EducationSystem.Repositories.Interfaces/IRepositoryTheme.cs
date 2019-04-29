using System.Collections.Generic;
using System.Threading.Tasks;
using EducationSystem.Database.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Repositories.Interfaces.Basics;

namespace EducationSystem.Repositories.Interfaces
{
    public interface IRepositoryTheme : IRepository<DatabaseTheme>
    {
        Task<(int Count, List<DatabaseTheme> Themes)> GetThemes(FilterTheme filter);
        Task<(int Count, List<DatabaseTheme> Themes)> GetThemesByTestId(int testId, FilterTheme filter);
        Task<(int Count, List<DatabaseTheme> Themes)> GetThemesByDisciplineId(int disciplineId, FilterTheme filter);

        Task<bool> IsThemeExists(int id);

        Task<int> GetLastThemeOrder(int disciplineId);
    }
}