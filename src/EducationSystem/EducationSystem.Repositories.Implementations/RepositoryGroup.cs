using System;
using System.Collections.Generic;
using System.Linq;
using EducationSystem.Database.Contexts;
using EducationSystem.Database.Models;
using EducationSystem.Extensions;
using EducationSystem.Models.Filters;
using EducationSystem.Repositories.Implementations.Basics;
using EducationSystem.Repositories.Interfaces;

namespace EducationSystem.Repositories.Implementations
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

        public DatabaseGroup GetGroupByStudentId(int studentId)
        {
            return AsQueryable()
                .FirstOrDefault(x => x.GroupStudents
                    .Any(y => y.Student.Id == studentId));
        }
    }
}