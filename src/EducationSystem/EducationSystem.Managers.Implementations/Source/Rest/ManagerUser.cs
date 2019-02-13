using System;
using AutoMapper;
using System.Collections.Generic;
using EducationSystem.Constants.Source;
using EducationSystem.Database.Models.Source;
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
        protected IRepositoryRole RepositoryRole { get; }
        protected IRepositoryUser RepositoryUser { get; }

        public ManagerUser(
            IMapper mapper,
            ILogger<ManagerUser> logger,
            IRepositoryUser repositoryUser,
            IRepositoryRole repositoryRole)
            : base(mapper, logger)
        {
            RepositoryUser = repositoryUser;
            RepositoryRole = repositoryRole;
        }

        public PagedData<User> GetUsers(OptionsUser options)
        {
            var (count, users) = RepositoryUser.GetUsers(options);

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
                    string.Format(Messages.User.NotFoundById, id),
                    new EducationSystemPublicException(Messages.User.NotFoundPublic));

            return Mapper.Map<User>(user);
        }
    }
}