using EducationSystem.Models;
using EducationSystem.Models.Source;

namespace EducationSystem.Managers.Interfaces.Source
{
    public interface IManagerToken
    {
        TokenResponse GenerateToken(TokenRequest request);
    }
}