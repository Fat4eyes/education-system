using System.Collections.Generic;
using System.Linq;
using EducationSystem.Database.Models.Source;
using EducationSystem.Database.Source.Contexts;
using EducationSystem.Extensions.Source;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Repositories.Interfaces.Source.Rest;

namespace EducationSystem.Repositories.Implementations.Source.Rest
{
    public class RepositoryStudent : RepositoryReadOnly<DatabaseUser>, IRepositoryStudent
    {
        public RepositoryStudent(DatabaseContext context)
            : base(context) { }

        public (int Count, List<DatabaseUser> Students) GetStudents(Filter filter) =>
            AsQueryable().ApplyPaging(filter);

        public (int Count, List<DatabaseUser> Students) GetStudentsByGroupId(int groupId, Filter filter)
        {
            return AsQueryable()
                .Where(x => x.StudentGroup.GroupId == groupId)
                .ApplyPaging(filter);
        }
    }
}