using System;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Models.Rest;
using EducationSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;

namespace EducationSystem.Implementations
{
    public sealed class ExecutionContext : IExecutionContext
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
        private readonly IExceptionFactory _exceptionFactory;
        private readonly IRepositoryUser _repositoryUser;

        private Lazy<User> Lazy { get; }

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

            Lazy = new Lazy<User>(GetCurrentUserInternal);
        }

        public User GetCurrentUser()
        {
            return Lazy.Value;
        }

        public int GetCurrentUserId()
        {
            return GetCurrentUser().Id;
        }

        private User GetCurrentUserInternal()
        {
            var value = _accessor.HttpContext?.User?.Claims?
                .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value ??
                    throw new ApplicationException("Не удалось получить идентификатор пользователя.");

            var userId = Convert.ToInt32(value);

            var user = _repositoryUser.GetByIdAsync(userId) ??
                throw _exceptionFactory.NotFound<DatabaseUser>(userId);

            return _mapper.Map<User>(user);
        }
    }
}