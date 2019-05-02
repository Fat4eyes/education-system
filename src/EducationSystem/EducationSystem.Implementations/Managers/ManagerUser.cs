using System.Threading.Tasks;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Interfaces.Managers;
using EducationSystem.Models.Rest;

namespace EducationSystem.Implementations.Managers
{
    public sealed class ManagerUser : Manager, IManagerUser
    {
        public ManagerUser(
            IExecutionContext executionContext,
            IExceptionFactory exceptionFactory)
            : base(
                executionContext,
                exceptionFactory)
        { }

        public async Task<User> GetCurrentUserAsync()
        {
            if (CurrentUser.IsAdmin() || CurrentUser.IsLecturer() || CurrentUser.IsStudent())
                return await ExecutionContext.GetCurrentUserAsync();

            throw ExceptionFactory.NoAccess();
        }
    }
}