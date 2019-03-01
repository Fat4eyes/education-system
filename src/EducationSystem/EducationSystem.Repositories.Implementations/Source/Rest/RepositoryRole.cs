using System.Collections.Generic;
using System.Linq;
using EducationSystem.Database.Models.Source;
using EducationSystem.Database.Source.Contexts;
using EducationSystem.Extensions.Source;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Repositories.Interfaces.Source.Rest;

namespace EducationSystem.Repositories.Implementations.Source.Rest
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