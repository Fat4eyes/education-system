using System.Collections.Generic;
using EducationSystem.Database.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Repositories.Interfaces.Basics;

namespace EducationSystem.Repositories.Interfaces
{
    public interface IRepositoryRole : IRepositoryReadOnly<DatabaseRole>
    {
        (int Count, List<DatabaseRole> Roles) GetRoles(FilterRole filter);

        DatabaseRole GetRoleByUserId(int userId);
    }
}