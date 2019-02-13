using System.Collections.Generic;
using EducationSystem.Database.Models.Source;
using EducationSystem.Models.Source.Options;

namespace EducationSystem.Repositories.Interfaces.Source.Rest
{
    public interface IRepositoryStudent : IRepositoryReadOnly<DatabaseUser>
    {
        (int Count, List<DatabaseUser> Students) GetStudents(OptionsStudent options);

        (int Count, List<DatabaseUser> Students) GetStudentsByGroupId(int groupId, OptionsStudent options);

        DatabaseUser GetStudentById(int id, OptionsStudent options);
    }
}