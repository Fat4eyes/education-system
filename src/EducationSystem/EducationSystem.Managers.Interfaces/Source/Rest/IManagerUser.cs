using EducationSystem.Models.Source.Rest;

namespace EducationSystem.Managers.Interfaces.Source.Rest
{
    public interface IManagerUser
    {
        User GetUserByEmail(string email);
    }
}