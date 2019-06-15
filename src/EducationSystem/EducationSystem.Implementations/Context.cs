using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Extensions;
using EducationSystem.Helpers;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Models.Rest;
using EducationSystem.Specifications.Users;
using Microsoft.AspNetCore.Http;

namespace EducationSystem.Implementations
{
    public sealed class Context : IContext
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
        private readonly IRepository<DatabaseUser> _repositoryUser;

        private Lazy<Task<User>> Lazy { get; }

        public Context(
            IMapper mapper,
            IHttpContextAccessor accessor,
            IRepository<DatabaseUser> repositoryUser)
        {
            _mapper = mapper;
            _accessor = accessor;
            _repositoryUser = repositoryUser;

            Lazy = new Lazy<Task<User>>(GetCurrentUserInternalAsync);
        }

        public User GetCurrentUser()
        {
            return GetCurrentUserAsync().WaitTask();
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

            var user = await _repositoryUser.FindFirstAsync(new UsersById(userId)) ??
                throw ExceptionHelper.NotFound<DatabaseUser>(userId);

            return _mapper.Map<User>(user);
        }
    }
}