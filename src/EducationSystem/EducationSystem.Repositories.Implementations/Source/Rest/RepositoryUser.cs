using System;
using System.Collections.Generic;
using System.Linq;
using EducationSystem.Database.Source;
using EducationSystem.Database.Models.Source;
using EducationSystem.Extensions.Source;
using EducationSystem.Models.Source.Options;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Microsoft.EntityFrameworkCore;

namespace EducationSystem.Repositories.Implementations.Source.Rest
{
    public class RepositoryUser : RepositoryReadOnly<DatabaseUser, OptionsUser>, IRepositoryUser
    {
        public RepositoryUser(EducationSystemDatabaseContext context)
            : base(context) { }

        public (int Count, List<DatabaseUser> Users) GetUsers(OptionsUser options) =>
            FilterByOptions(IncludeByOptions(AsQueryable(), options), options)
                .ApplyPaging(options);

        public (int Count, List<DatabaseUser> Users) GetUsersByRoleId(int roleId, OptionsUser options) =>
            FilterByOptions(IncludeByOptions(AsQueryable(), options), options)
                .Where(x => x.UserRoles.Any(y => y.RoleId == roleId))
                .ApplyPaging(options);

        public DatabaseUser GetUserById(int id, OptionsUser options) =>
            IncludeByOptions(AsQueryable(), options)
                .FirstOrDefault(x => x.Id == id);

        public DatabaseUser GetUserByEmail(string email, OptionsUser options) =>
            IncludeByOptions(AsQueryable(), options)
                .FirstOrDefault(x => string.Compare(x.Email, email, StringComparison.CurrentCultureIgnoreCase) == 0);

        protected override IQueryable<DatabaseUser> IncludeByOptions(IQueryable<DatabaseUser> query, OptionsUser options)
        {
            if (options.WithRoles)
            {
                query = query
                    .Include(x => x.UserRoles)
                        .ThenInclude(x => x.Role);
            }

            return query;
        }
    }
}