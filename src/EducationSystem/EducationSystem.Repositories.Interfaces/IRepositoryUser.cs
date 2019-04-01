using System.Collections.Generic;
using EducationSystem.Database.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Repositories.Interfaces.Basics;

namespace EducationSystem.Repositories.Interfaces
{
    public interface IRepositoryUser : IRepositoryReadOnly<DatabaseUser>
    {
        (int Count, List<DatabaseUser> Users) GetUsers(FilterUser filter);
        (int Count, List<DatabaseUser> Users) GetUsersByRoleId(int roleId, FilterUser filter);

        DatabaseUser GetUserByEmail(string email);
    }
}