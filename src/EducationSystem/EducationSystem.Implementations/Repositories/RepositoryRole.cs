using System.Linq;
using System.Threading.Tasks;
using EducationSystem.Database.Contexts;
using EducationSystem.Database.Models;
using EducationSystem.Implementations.Repositories.Basics;
using EducationSystem.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EducationSystem.Implementations.Repositories
{
    public sealed class RepositoryRole : RepositoryReadOnly<DatabaseRole>, IRepositoryRole
    {
        public RepositoryRole(DatabaseContext context) : base(context) { }

        public Task<DatabaseRole> GetUserRoleAsync(int userId)
        {
            return AsQueryable().FirstOrDefaultAsync(x => x.RoleUsers.Any(y => y.User.Id == userId));
        }
    }
}