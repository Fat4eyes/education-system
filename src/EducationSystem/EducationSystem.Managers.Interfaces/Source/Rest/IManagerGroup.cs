using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;

namespace EducationSystem.Managers.Interfaces.Source.Rest
{
    public interface IManagerGroup
    {
        PagedData<Group> GetGroups(OptionsGroup options);

        Group GetGroupById(int id, OptionsGroup options);
        Group GetGroupByStudentId(int studentId, OptionsGroup options);
    }
}