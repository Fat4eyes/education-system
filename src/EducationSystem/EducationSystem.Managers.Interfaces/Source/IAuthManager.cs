using EducationSystem.Models.Source;

namespace EducationSystem.Managers.Interfaces.Source
{
    public interface IAuthManager
    {
        SignInResponse SignIn(SignInRequest model);
    }
}