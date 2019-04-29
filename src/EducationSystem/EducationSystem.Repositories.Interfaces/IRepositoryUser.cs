using System.Collections.Generic;
using System.Threading.Tasks;
using EducationSystem.Database.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Repositories.Interfaces.Basics;

namespace EducationSystem.Repositories.Interfaces
{
    public interface IRepositoryUser : IRepositoryReadOnly<DatabaseUser>
    {
        Task<(int Count, List<DatabaseUser> Users)> GetUsers(FilterUser filter);
        Task<(int Count, List<DatabaseUser> Users)> GetUsersByRoleId(int roleId, FilterUser filter);

        Task<DatabaseUser> GetUserByEmail(string email);
    }
}