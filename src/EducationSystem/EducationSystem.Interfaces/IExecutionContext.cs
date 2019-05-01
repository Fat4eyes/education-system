using EducationSystem.Models.Rest;

namespace EducationSystem.Interfaces
{
    public interface IExecutionContext
    {
        User GetCurrentUser();

        int GetCurrentUserId();
    }
}