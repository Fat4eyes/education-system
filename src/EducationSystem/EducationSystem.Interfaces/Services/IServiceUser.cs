using System.Threading.Tasks;
using EducationSystem.Models.Rest;

namespace EducationSystem.Interfaces.Services
{
    public interface IServiceUser
    {
        Task<User> GetCurrentUserAsync();
    }
}