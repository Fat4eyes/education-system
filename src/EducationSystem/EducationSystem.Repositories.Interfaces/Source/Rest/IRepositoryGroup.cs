using System.Collections.Generic;
using EducationSystem.Database.Models.Source;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Models.Source.Options;

namespace EducationSystem.Repositories.Interfaces.Source.Rest
{
    public interface IRepositoryGroup : IRepositoryReadOnly<DatabaseGroup>
    {
        (int Count, List<DatabaseGroup> Groups) GetGroups(FilterGroup filter);

        DatabaseGroup GetGroupById(int id);

        DatabaseGroup GetGroupByStudentId(int studentId);
    }
}