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
            IContext context,
            ILogger<ServiceUser> logger)
            : base(mapper, context, logger)
        { }

        public async Task<User> GetCurrentUserAsync()
        {
            var user = await Context.GetCurrentUserAsync();

            if (user.IsAdmin() || user.IsLecturer() || user.IsStudent())
                return user;

            throw ExceptionHelper.NoAccess();
        }
    }
}