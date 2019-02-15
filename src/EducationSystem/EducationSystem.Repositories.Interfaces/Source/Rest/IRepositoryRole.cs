using System.Collections.Generic;
using EducationSystem.Database.Models.Source;
using EducationSystem.Models.Source.Options;

namespace EducationSystem.Repositories.Interfaces.Source.Rest
{
    public interface IRepositoryRole : IRepositoryReadOnly<DatabaseRole>
    {
        (int Count, List<DatabaseRole> Roles) GetRoles(OptionsRole options);

        DatabaseRole GetRoleById(int id, OptionsRole options);
        DatabaseRole GetRoleByUserId(int userId, OptionsRole options);
    }
}