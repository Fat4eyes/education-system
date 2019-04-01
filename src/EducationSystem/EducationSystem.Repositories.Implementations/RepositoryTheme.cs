using System.Collections.Generic;
using System.Linq;
using EducationSystem.Database.Models;
using EducationSystem.Database.Source.Contexts;
using EducationSystem.Extensions;
using EducationSystem.Models.Filters;
using EducationSystem.Repositories.Implementations.Basics;
using EducationSystem.Repositories.Interfaces;

namespace EducationSystem.Repositories.Implementations
{
    public sealed class RepositoryTheme : Repository<DatabaseTheme>, IRepositoryTheme
    {
        public RepositoryTheme(DatabaseContext context)
            : base(context) { }

        public (int Count, List<DatabaseTheme> Themes) GetThemes(FilterTheme filter) =>
            AsQueryable().ApplyPaging(filter);

        public (int Count, List<DatabaseTheme> Themes) GetThemesByTestId(int testId, FilterTheme filter)
        {
            return AsQueryable()
                .Where(x => x.ThemeTests.Any(y => y.TestId == testId))
                .ApplyPaging(filter);
        }

        public (int Count, List<DatabaseTheme> Themes) GetThemesByDisciplineId(int disciplineId, FilterTheme filter)
        {
            return AsQueryable()
                .Where(x => x.DisciplineId == disciplineId)
                .ApplyPaging(filter);
        }

        public bool IsThemesExists(List<int> themeIds)
        {
            themeIds = themeIds
                .Distinct()
                .ToList();

            return AsQueryable().Count(x => themeIds.Contains(x.Id)) == themeIds.Count;
        }
    }
}