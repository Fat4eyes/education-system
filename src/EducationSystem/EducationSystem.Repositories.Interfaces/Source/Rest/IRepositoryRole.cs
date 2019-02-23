using System.Collections.Generic;
using EducationSystem.Database.Models.Source;
using EducationSystem.Models.Source.Filters;

namespace EducationSystem.Repositories.Interfaces.Source.Rest
{
    public interface IRepositoryRole : IRepositoryReadOnly<DatabaseRole>
    {
        (int Count, List<DatabaseRole> Roles) GetRoles(Filter filter);

        DatabaseRole GetRoleByUserId(int userId);
    }
}