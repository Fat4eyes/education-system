using System.Threading.Tasks;
using EducationSystem.Models;

namespace EducationSystem.Interfaces
{
    public interface ITokenGenerator
    {
        Task<TokenResponse> GenerateTokenAsync(TokenRequest request);
    }
}