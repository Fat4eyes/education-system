using System.Collections.Generic;
using System.Linq;
using EducationSystem.Database.Models.Source;
using EducationSystem.Database.Source;
using EducationSystem.Extensions.Source;
using EducationSystem.Models.Source.Options;
using EducationSystem.Repositories.Interfaces.Source.Rest;

namespace EducationSystem.Repositories.Implementations.Source.Rest
{
    public class RepositoryRole : RepositoryReadOnly<DatabaseRole, OptionsRole>, IRepositoryRole
    {
        public RepositoryRole(EducationSystemDatabaseContext context)
            : base(context) { }

        public (int Count, List<DatabaseRole> Roles) GetRoles(OptionsRole options) =>
            FilterByOptions(IncludeByOptions(AsQueryable(), options), options)
                .ApplyPaging(options);

        public DatabaseRole GetRoleByUserId(int userId, OptionsRole options) =>
            IncludeByOptions(AsQueryable(), options)
                .FirstOrDefault(x => x.RoleUsers.Any(y => y.User.Id == userId));
    }
}