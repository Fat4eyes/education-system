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

        public Task<(int Count, List<DatabaseTheme> Themes)> GetThemesAsync(FilterTheme filter)
        {
            return GetByFilter(filter)
                .OrderBy(x => x.Order)
                .ApplyPagingAsync(filter);
        }

        public Task<(int Count, List<DatabaseTheme> Themes)> GetLecturerThemesAsync(int lecturerId, FilterTheme filter)
        {
            return GetByFilter(filter)
                .Where(x => x.Discipline.Lecturers.Any(y => y.LecturerId == lecturerId))
                .OrderBy(x => x.Order)
                .ApplyPagingAsync(filter);
        }

        public Task<DatabaseTheme> GetLecturerThemeAsync(int id, int lecturerId)
        {
            return AsQueryable()
                .Where(x => x.Discipline.Lecturers.Any(y => y.LecturerId == lecturerId))
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        private IQueryable<DatabaseTheme> GetByFilter(FilterTheme filter)
        {
            var query = AsQueryable();

            if (filter.DisciplineId.HasValue)
                query = query.Where(x => x.DisciplineId == filter.DisciplineId);

            if (filter.TestId.HasValue)
                query = query.Where(x => x.ThemeTests.Any(y => y.TestId == filter.TestId));

            return query;
        }
    }
}