using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;

namespace EducationSystem.Managers.Interfaces.Source.Rest
{
    public interface IManagerUser
    {
        PagedData<User> GetUsers(OptionsUser options);

        PagedData<User> GetUsersByGroupId(int groupId, OptionsUser options);

        PagedData<User> GetUsersByRoleId(int roleId, OptionsUser options);

        User GetUserById(int id, OptionsUser options);
    }
}