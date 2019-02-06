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

        public (int Count, List<DatabaseUser> Users) GetUsers(OptionsUser options)
        {
            return FilterByOptions(GetQueryableWithInclusions(options), options).ApplyPaging(options);
        }

        public (int Count, List<DatabaseUser> Users) GetUsersByGroupId(int groupId, OptionsUser options)
        {
            return FilterByOptions(GetQueryableWithInclusions(options), options)
                .Where(x => x.StudentGroup.GroupId == groupId)
                .ApplyPaging(options);
        }

        public (int Count, List<DatabaseUser> Users) GetUsersByRoleId(int roleId, OptionsUser options)
        {
            return FilterByOptions(GetQueryableWithInclusions(options), options)
                .Where(x => x.UserRoles.Any(y => y.RoleId == roleId))
                .ApplyPaging(options);
        }

        public DatabaseUser GetUserById(int id, OptionsUser options)
        {
            return GetQueryableWithInclusions(options).FirstOrDefault(x => x.Id == id);
        }

        public DatabaseUser GetUserByEmail(string email, OptionsUser options)
        {
            return GetQueryableWithInclusions(options).FirstOrDefault(
                x => string.Compare(x.Email, email, StringComparison.CurrentCultureIgnoreCase) == 0);
        }

        protected override IQueryable<DatabaseUser> GetQueryableWithInclusions(OptionsUser options)
        {
            var query = AsQueryable();

            if (options.WithGroup)
            {
                query = query
                    .Include(x => x.StudentGroup)
                        .ThenInclude(x => x.Group);
            }

            if (options.WithRoles)
            {
                query = query
                    .Include(x => x.UserRoles)
                        .ThenInclude(x => x.Role);
            }

            if (options.WithTestResults)
            {
                query = query.Include(x => x.TestResults);
            }

            return query;
        }
    }
}