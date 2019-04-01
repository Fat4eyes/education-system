using System.Collections.Generic;
using System.Linq;
using EducationSystem.Database.Contexts;
using EducationSystem.Database.Models;
using EducationSystem.Extensions;
using EducationSystem.Models.Filters;
using EducationSystem.Repositories.Implementations.Basics;
using EducationSystem.Repositories.Interfaces;

namespace EducationSystem.Repositories.Implementations
{
    public sealed class RepositoryRole : RepositoryReadOnly<DatabaseRole>, IRepositoryRole
    {
        public RepositoryRole(DatabaseContext context)
            : base(context) { }

        public (int Count, List<DatabaseRole> Roles) GetRoles(FilterRole filter) =>
            AsQueryable().ApplyPaging(filter);

        public DatabaseRole GetRoleByUserId(int userId)
        {
            return AsQueryable()
                .FirstOrDefault(x => x.RoleUsers
                    .Any(y => y.User.Id == userId));
        }
    }
}