using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;

namespace EducationSystem.Managers.Interfaces.Source.Rest
{
    public interface IManagerUser
    {
        PagedData<User> GetUsers(OptionsUser options, Filter filter);
        PagedData<User> GetUsersByRoleId(int roleId, OptionsUser options, Filter filter);

        User GetUserById(int id, OptionsUser options);
    }
}