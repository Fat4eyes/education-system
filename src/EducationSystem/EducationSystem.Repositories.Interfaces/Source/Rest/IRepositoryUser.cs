﻿using System.Collections.Generic;
using EducationSystem.Database.Models.Source;
using EducationSystem.Models.Source.Filters;

namespace EducationSystem.Repositories.Interfaces.Source.Rest
{
    public interface IRepositoryUser : IRepositoryReadOnly<DatabaseUser>
    {
        (int Count, List<DatabaseUser> Users) GetUsers(FilterUser filter);
        (int Count, List<DatabaseUser> Users) GetUsersByRoleId(int roleId, FilterUser filter);

        DatabaseUser GetUserByEmail(string email);
    }
}