using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Exceptions.Helpers;
using EducationSystem.Interfaces.Managers;
using EducationSystem.Models.Options;
using EducationSystem.Models.Rest;
using EducationSystem.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Managers
{
    public sealed class ManagerUser : Manager<ManagerUser>, IManagerUser
    {
        private readonly IRepositoryUser _repositoryUser;

        public ManagerUser(
            IMapper mapper,
            ILogger<ManagerUser> logger,
            IRepositoryUser repositoryUser)
            : base(mapper, logger)
        {
            _repositoryUser = repositoryUser;
        }

        public async Task<User> GetUser(int id, OptionsUser options)
        {
            var user = await _repositoryUser.GetById(id) ??
                throw ExceptionHelper.CreateNotFoundException(
                    $"Пользователь не найден. Идентификатор пользователя: {id}.",
                    $"Пользователь не найден.");

            return Map(user, options);
        }

        private User Map(DatabaseUser user, OptionsUser options)
        {
            return Mapper.Map<DatabaseUser, User>(user, x =>
            {
                x.AfterMap((s, d) =>
                {
                    if (options.WithRoles)
                        d.Roles = Mapper.Map<List<Role>>(s.UserRoles.Select(y => y.Role));
                });
            });
        }
    }
}