using AutoMapper;
using System.Collections.Generic;
using EducationSystem.Exceptions.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Managers.Implementations.Source.Rest
{
    public class ManagerUser : Manager<ManagerUser>, IManagerUser
    {
        protected IRepositoryUser RepositoryUser { get; }

        public ManagerUser(
            IMapper mapper,
            ILogger<ManagerUser> logger,
            IRepositoryUser repositoryUser)
            : base(mapper, logger)
        {
            RepositoryUser = repositoryUser;
        }

        public PagedData<User> GetUsers(OptionsUser options)
        {
            var (count, users) = RepositoryUser.GetUsers(options);

            return new PagedData<User>(Mapper.Map<List<User>>(users), count);
        }

        public PagedData<User> GetUsersByGroupId(int groupId, OptionsUser options)
        {
            var (count, users) = RepositoryUser.GetUsersByGroupId(groupId, options);

            return new PagedData<User>(Mapper.Map<List<User>>(users), count);
        }

        public PagedData<User> GetUsersByRoleId(int roleId, OptionsUser options)
        {
            var (count, users) = RepositoryUser.GetUsersByRoleId(roleId, options);

            return new PagedData<User>(Mapper.Map<List<User>>(users), count);
        }

        public User GetUserById(int id, OptionsUser options)
        {
            var user = RepositoryUser.GetUserById(id, options) ??
                throw new EducationSystemException(
                    $"Пользователь не найден. Идентификатор: {id}.",
                    new EducationSystemPublicException("Пользователь не найден."));

            return Mapper.Map<User>(user);
        }
    }
}