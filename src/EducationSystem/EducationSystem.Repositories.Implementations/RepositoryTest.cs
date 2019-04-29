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
    public sealed class RepositoryTest : Repository<DatabaseTest>, IRepositoryTest
    {
        public RepositoryTest(DatabaseContext context)
            : base(context) { }

        public async Task<(int Count, List<DatabaseTest> Tests)> GetTests(FilterTest filter)
        {
            var query = AsQueryable();

            if (filter.DisciplineId.HasValue)
                query = query.Where(x => x.DisciplineId == filter.DisciplineId);

            if (filter.OnlyActive)
                query = query.Where(x => x.IsActive == 1);

            if (filter.TestType.HasValue)
                query = query.Where(x => x.Type == filter.TestType.Value);

            if (string.IsNullOrWhiteSpace(filter.Name) == false)
                query = query.Where(x => x.Subject.Contains(filter.Name, StringComparison.CurrentCultureIgnoreCase));
                
            return await query.ApplyPaging(filter);
        }

        public async Task<(int Count, List<DatabaseTest> Tests)> GetTestsByDisciplineId(int disciplineId, FilterTest filter)
        {
            var query = AsQueryable()
                .Where(x => x.DisciplineId == disciplineId);

            if (filter.OnlyActive)
                query = query.Where(x => x.IsActive == 1);

            if (filter.TestType.HasValue)
                query = query.Where(x => x.Type == filter.TestType.Value);

            if (string.IsNullOrWhiteSpace(filter.Name) == false)
                query = query.Where(x => x.Subject.Contains(filter.Name, StringComparison.CurrentCultureIgnoreCase));

            return await query.ApplyPaging(filter);
        }

        public async Task<(int Count, List<DatabaseTest> Tests)> GetTestsForStudent(int studentId, FilterTest filter)
        {
            var query = AsQueryable()
                .Where(x => x.IsActive == 1 && x.TestThemes.Any(y => y.Theme.Questions.Any()))
                .Where(x => x.Discipline.StudyProfiles
                    .Any(a => a.StudyProfile.StudyPlans
                    .Any(b => b.Groups
                    .Any(c => c.GroupStudents
                    .Any(d => d.StudentId == studentId)))));

            if (filter.DisciplineId.HasValue)
                query = query.Where(x => x.DisciplineId == filter.DisciplineId);

            if (filter.TestType.HasValue)
                query = query.Where(x => x.Type == filter.TestType.Value);

            if (string.IsNullOrWhiteSpace(filter.Name) == false)
                query = query.Where(x => x.Subject.Contains(filter.Name, StringComparison.CurrentCultureIgnoreCase));

            return await query.ApplyPaging(filter);
        }

        public Task<DatabaseTest> GetTestForStudentById(int id, int studentId)
        {
            return AsQueryable()
                .Where(x => x.Id == id)
                .Where(x => x.IsActive == 1)
                .Where(x => x.TestThemes.Any(y => y.Theme.Questions.Any()))
                .FirstOrDefaultAsync(x => x.Discipline.StudyProfiles
                    .Any(a => a.StudyProfile.StudyPlans
                    .Any(b => b.Groups
                    .Any(c => c.GroupStudents
                    .Any(d => d.StudentId == studentId)))));
        }
    }
}