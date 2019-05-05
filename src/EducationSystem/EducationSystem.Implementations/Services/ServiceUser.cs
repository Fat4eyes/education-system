using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Interfaces.Services;
using EducationSystem.Models.Rest;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Services
{
    public sealed class ServiceUser : Service<ServiceUser>, IServiceUser
    {
        public ServiceUser(
            IMapper mapper,
            ILogger<ServiceUser> logger,
            IExecutionContext executionContext,
            IExceptionFactory exceptionFactory)
            : base(
                mapper,
                logger,
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