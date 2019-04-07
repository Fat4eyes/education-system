using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.Models.Rest;

namespace EducationSystem.Interfaces.Managers
{
    public interface IManagerUser
    {
        PagedData<User> GetUsers(OptionsUser options, FilterUser filter);
        PagedData<User> GetUsersByRoleId(int roleId, OptionsUser options, FilterUser filter);

        User GetUserById(int id, OptionsUser options);
    }
}