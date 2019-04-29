using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationSystem.Database.Contexts;
using EducationSystem.Database.Models;
using EducationSystem.Extensions;
using EducationSystem.Models.Filters;
using EducationSystem.Repositories.Implementations.Basics;
using EducationSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EducationSystem.Repositories.Implementations
{
    public sealed class RepositoryTheme : Repository<DatabaseTheme>, IRepositoryTheme
    {
        public RepositoryTheme(DatabaseContext context)
            : base(context) { }

        public Task<(int Count, List<DatabaseTheme> Themes)> GetThemes(FilterTheme filter)
        {
            return AsQueryable()
                .OrderBy(x => x.Order)
                .ApplyPaging(filter);
        }

        public Task<(int Count, List<DatabaseTheme> Themes)> GetThemesByTestId(int testId, FilterTheme filter)
        {
            return AsQueryable()
                .Where(x => x.ThemeTests.Any(y => y.TestId == testId))
                .OrderBy(x => x.Order)
                .ApplyPaging(filter);
        }

        public Task<(int Count, List<DatabaseTheme> Themes)> GetThemesByDisciplineId(int disciplineId, FilterTheme filter)
        {
            return AsQueryable()
                .Where(x => x.DisciplineId == disciplineId)
                .OrderBy(x => x.Order)
                .ApplyPaging(filter);
        }

        public Task<bool> IsThemeExists(int id)
        {
            return AsQueryable().AnyAsync(x => x.Id == id);
        }

        public Task<int> GetLastThemeOrder(int disciplineId)
        {
            return AsQueryable()
                .Where(x => x.DisciplineId == disciplineId && x.Order.HasValue)
                .MaxAsync(x => x.Order.Value);
        }
    }
}