using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Helpers;
using EducationSystem.Interfaces;
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
            IExecutionContext executionContext)
            : base(
                mapper,
                logger,
                executionContext)
        { }

        public async Task<User> GetCurrentUserAsync()
        {
            var user = await ExecutionContext.GetCurrentUserAsync();

            if (user.IsAdmin() || user.IsLecturer() || user.IsStudent())
                return user;

            throw ExceptionHelper.NoAccess();
        }
    }
}