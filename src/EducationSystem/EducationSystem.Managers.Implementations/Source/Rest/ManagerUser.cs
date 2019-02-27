using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using EducationSystem.Constants.Source;
using EducationSystem.Database.Models.Source;
using EducationSystem.Exceptions.Source.Helpers;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Managers.Implementations.Source.Rest
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

        public PagedData<User> GetUsers(OptionsUser options, Filter filter)
        {
            var (count, users) = _repositoryUser.GetUsers(filter);

            return new PagedData<User>(users.Select(x => Map(x, options)).ToList(), count);
        }

        public PagedData<User> GetUsersByRoleId(int roleId, OptionsUser options, Filter filter)
        {
            var (count, users) = _repositoryUser.GetUsersByRoleId(roleId, filter);

            return new PagedData<User>(users.Select(x => Map(x, options)).ToList(), count);
        }

        public User GetUserById(int id, OptionsUser options)
        {
            var user = _repositoryUser.GetById(id) ??
                throw ExceptionHelper.CreateNotFoundException(
                    Messages.User.NotFoundById(id),
                    Messages.User.NotFoundPublic);

            return Mapper.Map<User>(Map(user, options));
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