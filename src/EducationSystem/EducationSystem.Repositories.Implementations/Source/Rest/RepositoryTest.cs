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
    public class RepositoryTest : Repository<DatabaseTest>, IRepositoryTest
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
    }
}