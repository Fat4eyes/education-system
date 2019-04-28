using System.Collections.Generic;
using EducationSystem.Database.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Repositories.Interfaces.Basics;

namespace EducationSystem.Repositories.Interfaces
{
    public interface IRepositoryTheme : IRepository<DatabaseTheme>
    {
        (int Count, List<DatabaseTheme> Themes) GetThemes(FilterTheme filter);
        (int Count, List<DatabaseTheme> Themes) GetThemesByTestId(int testId, FilterTheme filter);
        (int Count, List<DatabaseTheme> Themes) GetThemesByDisciplineId(int disciplineId, FilterTheme filter);

        bool IsThemeExists(int id);

        int GetLastThemeOrder(int disciplineId);
    }
}