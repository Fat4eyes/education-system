using System.Threading.Tasks;
using EducationSystem.Database.Models;
using EducationSystem.Repositories.Interfaces.Basics;

namespace EducationSystem.Repositories.Interfaces
{
    public interface IRepositoryRole : IRepositoryReadOnly<DatabaseRole>
    {
        Task<DatabaseRole> GetRoleByUserId(int userId);
    }
}