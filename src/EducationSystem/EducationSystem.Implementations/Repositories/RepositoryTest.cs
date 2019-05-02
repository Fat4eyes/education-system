using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationSystem.Database.Contexts;
using EducationSystem.Database.Models;
using EducationSystem.Extensions;
using EducationSystem.Implementations.Repositories.Basics;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Models.Filters;
using Microsoft.EntityFrameworkCore;

namespace EducationSystem.Implementations.Repositories
{
    public sealed class RepositoryTest : Repository<DatabaseTest>, IRepositoryTest
    {
        public RepositoryTest(DatabaseContext context) : base(context) { }

        public Task<(int Count, List<DatabaseTest> Tests)> GetTestsAsync(FilterTest filter)
        {
            return GetByFilter(filter).ApplyPagingAsync(filter);
        }

        public Task<(int Count, List<DatabaseTest> Tests)> GetStudentTestsAsync(int studentId, FilterTest filter)
        {
            return GetByFilter(filter)
                .Where(x => x.Discipline.StudyProfiles
                    .Any(a => a.StudyProfile.StudyPlans
                    .Any(b => b.Groups
                    .Any(c => c.GroupStudents
                    .Any(d => d.StudentId == studentId)))))
                .ApplyPagingAsync(filter);
        }

        public Task<(int Count, List<DatabaseTest> Tests)> GetLecturerTestsAsync(int lecturerId, FilterTest filter)
        {
            return GetByFilter(filter)
                .Where(x => x.Discipline.Lecturers.Any(y => y.LecturerId == lecturerId))
                .ApplyPagingAsync(filter);
        }

        public Task<DatabaseTest> GetStudentTestAsync(int id, int studentId)
        {
            return AsQueryable()
                .Where(x => x.Discipline.StudyProfiles
                    .Any(a => a.StudyProfile.StudyPlans
                    .Any(b => b.Groups
                    .Any(c => c.GroupStudents
                    .Any(d => d.StudentId == studentId)))))
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<DatabaseTest> GetLecturerTestAsync(int id, int lecturerId)
        {
            return AsQueryable()
                .Where(x => x.Discipline.Lecturers.Any(y => y.LecturerId == lecturerId))
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        private IQueryable<DatabaseTest> GetByFilter(FilterTest filter)
        {
            var query = AsQueryable();

            if (filter.DisciplineId.HasValue)
                query = query.Where(x => x.DisciplineId == filter.DisciplineId);

            if (filter.OnlyActive)
                query = query.Where(x => x.IsActive == 1);

            if (filter.TestType.HasValue)
                query = query.Where(x => x.Type == filter.TestType.Value);

            if (string.IsNullOrWhiteSpace(filter.Name) == false)
                query = query.Where(x => x.Subject.Contains(filter.Name, StringComparison.InvariantCultureIgnoreCase));

            return query;
        }
    }
}