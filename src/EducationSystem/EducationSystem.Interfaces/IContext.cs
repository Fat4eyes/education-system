using System.Threading.Tasks;
using EducationSystem.Models.Rest;

namespace EducationSystem.Interfaces
{
    public interface IContext
    {
        User GetCurrentUser();
        Task<User> GetCurrentUserAsync();
    }
}