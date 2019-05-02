using System.Threading.Tasks;
using EducationSystem.Models.Rest;

namespace EducationSystem.Interfaces
{
    public interface IExecutionContext
    {
        Task<User> GetCurrentUserAsync();
    }
}