using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Rest;

namespace EducationSystem.Interfaces.Managers.Rest
{
    public interface IManagerRole
    {
        PagedData<Role> GetRoles(OptionsRole options, FilterRole filter);

        Role GetRoleById(int id, OptionsRole options);
        Role GetRoleByUserId(int userId, OptionsRole options);
    }
}