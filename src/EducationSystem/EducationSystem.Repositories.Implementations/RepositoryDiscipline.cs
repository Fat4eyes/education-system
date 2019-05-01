using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationSystem.Database.Contexts;
using EducationSystem.Database.Models;
using EducationSystem.Extensions;
using EducationSystem.Models.Filters;
using EducationSystem.Repositories.Implementations.Basics;
using EducationSystem.Repositories.Interfaces;

namespace EducationSystem.Repositories.Implementations
{
    public sealed class RepositoryDiscipline : RepositoryReadOnly<DatabaseDiscipline>, IRepositoryDiscipline
    {
        public RepositoryDiscipline(DatabaseContext context)
            : base(context) { }

        public Task<(int Count, List<DatabaseDiscipline> Disciplines)> GetDisciplinesAsync(FilterDiscipline filter)
        {
            var query = AsQueryable();

            if (string.IsNullOrWhiteSpace(filter.Name) == false)
                query = query.Where(x => x.Name.Contains(filter.Name, StringComparison.CurrentCultureIgnoreCase));

            return query.ApplyPagingAsync(filter);
        }

        public Task<(int Count, List<DatabaseDiscipline> Disciplines)> GetDisciplinesByStudentIdAsync(int studentId, FilterDiscipline filter)
        {
            var query = AsQueryable()
                .Where(x => x.Themes.Any(y => y.Questions.Any()))
                .Where(x => x.Tests.Any(y => y.IsActive == 1 && y.TestThemes.Any(z => z.Theme.Questions.Any())))
                .Where(x => x.StudyProfiles
                    .Any(a => a.StudyProfile.StudyPlans
                    .Any(b => b.Groups
                    .Any(c => c.GroupStudents
                    .Any(d => d.StudentId == studentId)))));

            if (string.IsNullOrWhiteSpace(filter.Name) == false)
                query = query.Where(x => x.Name.Contains(filter.Name, StringComparison.CurrentCultureIgnoreCase));

            return query.ApplyPagingAsync(filter);
        }
    }
}