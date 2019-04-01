using System.Collections.Generic;
using EducationSystem.Database.Models.Source;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Repositories.Interfaces.Basics;

namespace EducationSystem.Repositories.Interfaces
{
    public interface IRepositoryGroup : IRepositoryReadOnly<DatabaseGroup>
    {
        (int Count, List<DatabaseGroup> Groups) GetGroups(FilterGroup filter);

        DatabaseGroup GetGroupByStudentId(int studentId);
    }
}