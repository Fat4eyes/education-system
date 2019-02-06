using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;

namespace EducationSystem.Managers.Interfaces.Source.Rest
{
    public interface IManagerRole
    {
        PagedData<Role> GetRoles(OptionsRole options);

        Role GetRoleByUserId(int userId, OptionsRole options);

        Role GetRoleById(int id, OptionsRole options);
    }
}