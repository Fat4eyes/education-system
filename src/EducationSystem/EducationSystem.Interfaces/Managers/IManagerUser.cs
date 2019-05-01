using System.Threading.Tasks;
using EducationSystem.Models.Options;
using EducationSystem.Models.Rest;

namespace EducationSystem.Interfaces.Managers
{
    public interface IManagerUser
    {
        Task<User> GetUserAsync(int id, OptionsUser options);
    }
}