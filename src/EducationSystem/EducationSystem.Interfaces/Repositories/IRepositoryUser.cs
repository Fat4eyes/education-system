using System.Threading.Tasks;
using EducationSystem.Database.Models;
using EducationSystem.Interfaces.Repositories.Basics;

namespace EducationSystem.Interfaces.Repositories
{
    public interface IRepositoryUser : IRepositoryReadOnly<DatabaseUser>
    {
        Task<DatabaseUser> GetUserByEmailAsync(string email);
    }
}