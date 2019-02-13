using System.Collections.Generic;
using EducationSystem.Database.Models.Source;
using EducationSystem.Models.Source.Options;

namespace EducationSystem.Repositories.Interfaces.Source.Rest
{
    public interface IRepositoryGroup : IRepositoryReadOnly<DatabaseGroup>
    {
        (int Count, List<DatabaseGroup> Groups) GetGroups(OptionsGroup options);

        DatabaseGroup GetGroupById(int id, OptionsGroup options);

        DatabaseGroup GetGroupByStudentId(int studentId, OptionsGroup options);
    }
}