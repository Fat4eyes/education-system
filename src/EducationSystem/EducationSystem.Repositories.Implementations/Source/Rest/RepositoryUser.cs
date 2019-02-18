﻿using System;
using System.Collections.Generic;
using System.Linq;
using EducationSystem.Database.Models.Source;
using EducationSystem.Database.Source.Contexts;
using EducationSystem.Extensions.Source;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Models.Source.Options;
using EducationSystem.Repositories.Interfaces.Source.Rest;

namespace EducationSystem.Repositories.Implementations.Source.Rest
{
    public class RepositoryUser : RepositoryReadOnly<DatabaseUser>, IRepositoryUser
    {
        public RepositoryUser(DatabaseContext context)
            : base(context) { }

        public (int Count, List<DatabaseUser> Users) GetUsers(Filter filter) =>
            AsQueryable().ApplyPaging(filter);

        public (int Count, List<DatabaseUser> Users) GetUsersByRoleId(int roleId, Filter filter) =>
            AsQueryable().Where(x => x.UserRoles.Any(y => y.RoleId == roleId))
                .ApplyPaging(filter);

        public DatabaseUser GetUserById(int id) => GetById(id);

        public DatabaseUser GetUserByEmail(string email, OptionsUser options) =>
            AsQueryable().FirstOrDefault(x => string.Equals(
                x.Email, email, StringComparison.CurrentCultureIgnoreCase));
    }
}