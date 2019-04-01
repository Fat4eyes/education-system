using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;

namespace EducationSystem.Interfaces.Managers.Rest
{
    public interface IManagerUser
    {
        PagedData<User> GetUsers(OptionsUser options, FilterUser filter);
        PagedData<User> GetUsersByRoleId(int roleId, OptionsUser options, FilterUser filter);

        User GetUserById(int id, OptionsUser options);
    }
}