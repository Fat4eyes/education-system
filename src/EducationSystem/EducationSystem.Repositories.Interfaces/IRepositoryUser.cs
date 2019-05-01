using System.Threading.Tasks;
using EducationSystem.Database.Models;
using EducationSystem.Repositories.Interfaces.Basics;

namespace EducationSystem.Repositories.Interfaces
{
    public interface IRepositoryUser : IRepositoryReadOnly<DatabaseUser>
    {
        Task<DatabaseUser> GetUserByEmailAsync(string email);
    }
}