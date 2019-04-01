using System.Collections.Generic;
using EducationSystem.Database.Models.Source;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Repositories.Interfaces.Basics;

namespace EducationSystem.Repositories.Interfaces
{
    public interface IRepositoryRole : IRepositoryReadOnly<DatabaseRole>
    {
        (int Count, List<DatabaseRole> Roles) GetRoles(FilterRole filter);

        DatabaseRole GetRoleByUserId(int userId);
    }
}