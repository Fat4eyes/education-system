using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.Models.Rest;

namespace EducationSystem.Interfaces.Managers.Rest
{
    public interface IManagerGroup
    {
        PagedData<Group> GetGroups(OptionsGroup options, FilterGroup filter);

        Group GetGroupById(int id, OptionsGroup options);
        Group GetGroupByStudentId(int studentId, OptionsGroup options);
    }
}