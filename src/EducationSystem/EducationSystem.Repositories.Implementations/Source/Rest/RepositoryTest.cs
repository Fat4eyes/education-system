using System;
using System.Collections.Generic;
using System.Linq;
using EducationSystem.Database.Models.Source;
using EducationSystem.Database.Source.Contexts;
using EducationSystem.Extensions.Source;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Repositories.Interfaces.Source.Rest;

namespace EducationSystem.Repositories.Implementations.Source.Rest
{
    public sealed class RepositoryTest : Repository<DatabaseTest>, IRepositoryTest
    {
        public RepositoryTest(DatabaseContext context)
            : base(context) { }

        public (int Count, List<DatabaseTest> Tests) GetTests(FilterTest filter)
        {
            var query = AsQueryable();

            if (filter.DisciplineId.HasValue)
                query = query.Where(x => x.DisciplineId == filter.DisciplineId);

            if (filter.OnlyActive)
                query = query.Where(x => x.IsActive == 1);

            if (string.IsNullOrWhiteSpace(filter.Name) == false)
                query = query.Where(x => x.Subject.Contains(filter.Name, StringComparison.CurrentCultureIgnoreCase));
                
            return query.ApplyPaging(filter);
        }

        public (int Count, List<DatabaseTest> Tests) GetTestsByDisciplineId(int disciplineId, FilterTest filter)
        {
            var query = AsQueryable().Where(x => x.DisciplineId == disciplineId);

            if (filter.OnlyActive)
                query = query.Where(x => x.IsActive == 1);

            if (string.IsNullOrWhiteSpace(filter.Name) == false)
                query = query.Where(x => x.Subject.Contains(filter.Name, StringComparison.CurrentCultureIgnoreCase));

            return query.ApplyPaging(filter);
        }

        public (int Count, List<DatabaseTest> Tests) GetTestsByStudentId(int studentId, FilterTest filter)
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

            if (string.IsNullOrWhiteSpace(filter.Name) == false)
                query = query.Where(x => x.Subject.Contains(filter.Name, StringComparison.CurrentCultureIgnoreCase));

            return query.ApplyPaging(filter);
        }
    }
}