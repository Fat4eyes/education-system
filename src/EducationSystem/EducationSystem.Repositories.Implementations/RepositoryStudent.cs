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
    public sealed class RepositoryStudent : RepositoryReadOnly<DatabaseUser>, IRepositoryStudent
    {
        public RepositoryStudent(DatabaseContext context)
            : base(context) { }

        public (int Count, List<DatabaseUser> Students) GetStudents(FilterStudent filter) =>
            AsQueryable().ApplyPaging(filter);

        public (int Count, List<DatabaseUser> Students) GetStudentsByGroupId(int groupId, FilterStudent filter)
        {
            return AsQueryable()
                .Where(x => x.StudentGroup.GroupId == groupId)
                .ApplyPaging(filter);
        }
    }
}