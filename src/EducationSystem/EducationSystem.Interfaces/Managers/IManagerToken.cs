using System.Threading.Tasks;
using EducationSystem.Models;

namespace EducationSystem.Interfaces.Managers
{
    public interface IManagerToken
    {
        Task<TokenResponse> GenerateToken(TokenRequest request);
    }
}