using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Models.Rest;
using Microsoft.AspNetCore.Http;

namespace EducationSystem.Implementations
{
    public sealed class ExecutionContext : IExecutionContext
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
        private readonly IExceptionFactory _exceptionFactory;
        private readonly IRepositoryUser _repositoryUser;

        private Lazy<Task<User>> Lazy { get; }

        public ExecutionContext(
            IMapper mapper,
            IHttpContextAccessor accessor,
            IExceptionFactory exceptionFactory,
            IRepositoryUser repositoryUser)
        {
            _mapper = mapper;
            _accessor = accessor;
            _exceptionFactory = exceptionFactory;
            _repositoryUser = repositoryUser;

            Lazy = new Lazy<Task<User>>(GetCurrentUserInternalAsync);
        }

        public async Task<User> GetCurrentUserAsync()
        {
            return await Lazy.Value;
        }

        private async Task<User> GetCurrentUserInternalAsync()
        {
            var value = _accessor.HttpContext?.User?.Claims?
                .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value ??
                    throw new ApplicationException("Не удалось получить идентификатор пользователя.");

            var userId = Convert.ToInt32(value);

            var user = await _repositoryUser.GetByIdAsync(userId) ??
                throw _exceptionFactory.NotFound<DatabaseUser>(userId);

            return _mapper.Map<User>(user);
        }
    }
}