using System.Collections.Generic;
using EducationSystem.Database.Models.Source;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Models.Source.Options;

namespace EducationSystem.Repositories.Interfaces.Source.Rest
{
    public interface IRepositoryStudent : IRepositoryReadOnly<DatabaseUser>
    {
        (int Count, List<DatabaseUser> Students) GetStudents(Filter filter);

        (int Count, List<DatabaseUser> Students) GetStudentsByGroupId(int groupId, Filter filter);

        DatabaseUser GetStudentById(int id);
    }
}