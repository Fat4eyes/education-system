using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Models.Source.Options;
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