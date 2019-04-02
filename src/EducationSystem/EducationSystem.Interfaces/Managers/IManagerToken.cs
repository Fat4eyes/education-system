using EducationSystem.Models;

namespace EducationSystem.Interfaces.Managers
{
    public interface IManagerToken
    {
        TokenResponse GenerateToken(TokenRequest request);
    }
}