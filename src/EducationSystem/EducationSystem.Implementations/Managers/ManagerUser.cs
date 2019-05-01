using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Interfaces.Managers;
using EducationSystem.Models.Rest;
using EducationSystem.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Managers
{
    public sealed class ManagerUser : Manager<ManagerUser>, IManagerUser
    {
        private readonly IExceptionFactory _exceptionFactory;
        private readonly IRepositoryUser _repositoryUser;

        public ManagerUser(
            IMapper mapper,
            ILogger<ManagerUser> logger,
            IExceptionFactory exceptionFactory,
            IRepositoryUser repositoryUser)
            : base(mapper, logger)
        {
            _exceptionFactory = exceptionFactory;
            _repositoryUser = repositoryUser;
        }

        public async Task<User> GetUserAsync(int id)
        {
            var user = await _repositoryUser.GetByIdAsync(id) ??
                throw _exceptionFactory.NotFound<DatabaseUser>(id);

            return Mapper.Map<User>(user);
        }
    }
}