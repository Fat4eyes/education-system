using System.Linq;
using System.Threading.Tasks;
using EducationSystem.Database.Contexts;
using EducationSystem.Database.Models;
using EducationSystem.Repositories.Implementations.Basics;
using EducationSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EducationSystem.Repositories.Implementations
{
    public sealed class RepositoryRole : RepositoryReadOnly<DatabaseRole>, IRepositoryRole
    {
        public RepositoryRole(DatabaseContext context)
            : base(context) { }

        public Task<DatabaseRole> GetRoleByUserId(int userId)
        {
            return AsQueryable().FirstOrDefaultAsync(x => x.RoleUsers.Any(y => y.User.Id == userId));
        }
    }
}