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
    public sealed class RepositoryQuestion : Repository<DatabaseQuestion>, IRepositoryQuestion
    {
        public RepositoryQuestion(DatabaseContext context)
            : base(context) { }

        public Task<(int Count, List<DatabaseQuestion> Questions)> GetQuestionsAsync(FilterQuestion filter)
        {
            var query = AsQueryable();

            if (filter.ThemeId.HasValue)
                query = query.Where(x => x.ThemeId == filter.ThemeId);

            return query
                .OrderBy(x => x.Order)
                .ApplyPagingAsync(filter);
        }

        public Task<(int Count, List<DatabaseQuestion> Questions)> GetLecturerQuestionsAsync(int lecturerId, FilterQuestion filter)
        {
            var query = AsQueryable()
                .Where(x => x.Theme.Discipline.Lecturers.Any(y => y.LecturerId == lecturerId));

            if (filter.ThemeId.HasValue)
                query = query.Where(x => x.ThemeId == filter.ThemeId);

            return query
                .OrderBy(x => x.Order)
                .ApplyPagingAsync(filter);
        }

        public Task<DatabaseQuestion> GetLecturerQuestionAsync(int id, int lecturerId)
        {
            return AsQueryable()
                .Where(x => x.Theme.Discipline.Lecturers.Any(y => y.LecturerId == lecturerId))
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}