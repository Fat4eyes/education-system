using System.Threading.Tasks;
using EducationSystem.Database.Models;
using EducationSystem.Interfaces.Repositories.Basics;

namespace EducationSystem.Interfaces.Repositories
{
    public interface IRepositoryRole : IRepositoryReadOnly<DatabaseRole>
    {
        Task<DatabaseRole> GetUserRoleAsync(int userId);
    }
}