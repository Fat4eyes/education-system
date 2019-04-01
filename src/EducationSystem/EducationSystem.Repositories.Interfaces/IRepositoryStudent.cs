using System.Collections.Generic;
using EducationSystem.Database.Models.Source;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Repositories.Interfaces.Basics;

namespace EducationSystem.Repositories.Interfaces
{
    public interface IRepositoryStudent : IRepositoryReadOnly<DatabaseUser>
    {
        (int Count, List<DatabaseUser> Students) GetStudents(FilterStudent filter);
        (int Count, List<DatabaseUser> Students) GetStudentsByGroupId(int groupId, FilterStudent filter);
    }
}