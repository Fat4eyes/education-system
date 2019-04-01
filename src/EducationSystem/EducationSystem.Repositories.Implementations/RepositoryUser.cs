using System;
using System.Collections.Generic;
using System.Linq;
using EducationSystem.Database.Models;
using EducationSystem.Database.Source.Contexts;
using EducationSystem.Extensions;
using EducationSystem.Models.Filters;
using EducationSystem.Repositories.Implementations.Basics;
using EducationSystem.Repositories.Interfaces;

namespace EducationSystem.Repositories.Implementations
{
    public sealed class RepositoryUser : RepositoryReadOnly<DatabaseUser>, IRepositoryUser
    {
        public RepositoryUser(DatabaseContext context)
            : base(context) { }

        public (int Count, List<DatabaseUser> Users) GetUsers(FilterUser filter) =>
            AsQueryable().ApplyPaging(filter);

        public (int Count, List<DatabaseUser> Users) GetUsersByRoleId(int roleId, FilterUser filter)
        {
            return AsQueryable()
                .Where(x => x.UserRoles.Any(y => y.RoleId == roleId))
                .ApplyPaging(filter);
        }

        public DatabaseUser GetUserByEmail(string email)
        {
            return AsQueryable()
                .FirstOrDefault(x => string.Equals(x.Email, email,
                    StringComparison.CurrentCultureIgnoreCase));
        }
    }
}