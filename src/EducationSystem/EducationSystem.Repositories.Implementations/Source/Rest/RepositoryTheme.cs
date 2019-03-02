using System.Collections.Generic;
using System.Linq;
using EducationSystem.Database.Models.Source;
using EducationSystem.Database.Source.Contexts;
using EducationSystem.Extensions.Source;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Repositories.Interfaces.Source.Rest;

namespace EducationSystem.Repositories.Implementations.Source.Rest
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

        public bool CheckThemesExistence(List<int> themeIds)
        {
            var count = AsQueryable().Count(x => themeIds.Contains(x.Id));

            return count == themeIds.Count;
        }
    }
}