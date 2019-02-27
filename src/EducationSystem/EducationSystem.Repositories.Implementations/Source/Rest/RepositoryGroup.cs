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
    public sealed class RepositoryGroup : RepositoryReadOnly<DatabaseGroup>, IRepositoryGroup
    {
        public RepositoryGroup(DatabaseContext context)
            : base(context) { }

        public (int Count, List<DatabaseGroup> Groups) GetGroups(FilterGroup filter)
        {
            var query = AsQueryable();

            if (string.IsNullOrWhiteSpace(filter.Name) == false)
                query = query.Where(x => x.Name.Contains(filter.Name, StringComparison.CurrentCultureIgnoreCase));

            return query.ApplyPaging(filter);
        }

        public DatabaseGroup GetGroupByStudentId(int studentId) =>
            AsQueryable().FirstOrDefault(x => x.GroupStudents.Any(y => y.Student.Id == studentId));
    }
}