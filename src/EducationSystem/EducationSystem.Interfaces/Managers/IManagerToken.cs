using EducationSystem.Models;
using EducationSystem.Models.Source;

namespace EducationSystem.Interfaces.Managers
{
    public interface IManagerToken
    {
        TokenResponse GenerateToken(TokenRequest request);
    }
}