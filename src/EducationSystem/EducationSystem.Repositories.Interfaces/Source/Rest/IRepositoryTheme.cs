using System.Collections.Generic;
using EducationSystem.Database.Models.Source;
using EducationSystem.Models.Source.Filters;

namespace EducationSystem.Repositories.Interfaces.Source.Rest
{
    public interface IRepositoryTheme : IRepository<DatabaseTheme>
    {
        (int Count, List<DatabaseTheme> Themes) GetThemes(FilterTheme filter);
        (int Count, List<DatabaseTheme> Themes) GetThemesByTestId(int testId, FilterTheme filter);
        (int Count, List<DatabaseTheme> Themes) GetThemesByDisciplineId(int disciplineId, FilterTheme filter);
    }
}