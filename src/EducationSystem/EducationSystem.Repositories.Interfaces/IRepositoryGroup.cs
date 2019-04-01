using System.Collections.Generic;
using EducationSystem.Database.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Repositories.Interfaces.Basics;

namespace EducationSystem.Repositories.Interfaces
{
    public interface IRepositoryGroup : IRepositoryReadOnly<DatabaseGroup>
    {
        (int Count, List<DatabaseGroup> Groups) GetGroups(FilterGroup filter);

        DatabaseGroup GetGroupByStudentId(int studentId);
    }
}