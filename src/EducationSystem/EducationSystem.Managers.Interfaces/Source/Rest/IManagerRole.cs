using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;

namespace EducationSystem.Managers.Interfaces.Source.Rest
{
    public interface IManagerRole
    {
        PagedData<Role> GetRoles(OptionsRole options, Filter filter);

        Role GetRoleById(int id, OptionsRole options);
        Role GetRoleByUserId(int userId, OptionsRole options);
    }
}