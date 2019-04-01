using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;

namespace EducationSystem.Interfaces.Managers.Rest
{
    public interface IManagerGroup
    {
        PagedData<Group> GetGroups(OptionsGroup options, FilterGroup filter);

        Group GetGroupById(int id, OptionsGroup options);
        Group GetGroupByStudentId(int studentId, OptionsGroup options);
    }
}