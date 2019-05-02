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
using Microsoft.EntityFrameworkCore;

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
                query = query.Where(x => x.Name.Contains(filter.Name, StringComparison.InvariantCultureIgnoreCase));

            return query.ApplyPagingAsync(filter);
        }

        public Task<(int Count, List<DatabaseDiscipline> Disciplines)> GetStudentDisciplinesAsync(int studentId, FilterDiscipline filter)
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
                query = query.Where(x => x.Name.Contains(filter.Name, StringComparison.InvariantCultureIgnoreCase));

            return query.ApplyPagingAsync(filter);
        }

        public Task<(int Count, List<DatabaseDiscipline> Disciplines)> GetLecturerDisciplinesAsync(int lecturerId, FilterDiscipline filter)
        {
            var query = AsQueryable()
                .Where(x => x.Lecturers.Any(y => y.LecturerId == lecturerId));

            if (string.IsNullOrWhiteSpace(filter.Name) == false)
                query = query.Where(x => x.Name.Contains(filter.Name, StringComparison.InvariantCultureIgnoreCase));

            return query.ApplyPagingAsync(filter);
        }

        public Task<DatabaseDiscipline> GetStudentDisciplineAsync(int id, int studentId)
        {
            return AsQueryable()
                .Where(x => x.Id == id)
                .Where(x => x.Themes.Any(y => y.Questions.Any()))
                .Where(x => x.Tests.Any(y => y.IsActive == 1 && y.TestThemes.Any(z => z.Theme.Questions.Any())))
                .FirstOrDefaultAsync(x => x.StudyProfiles
                    .Any(a => a.StudyProfile.StudyPlans
                    .Any(b => b.Groups
                    .Any(c => c.GroupStudents
                    .Any(d => d.StudentId == studentId)))));
        }

        public Task<DatabaseDiscipline> GetLecturerDisciplineAsync(int id, int lecturerId)
        {
            return AsQueryable()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync(x => x.Lecturers.Any(y => y.LecturerId == lecturerId));
        }
    }
}