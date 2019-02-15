using System.Collections.Generic;
using AutoMapper;
using EducationSystem.Constants.Source;
using EducationSystem.Exceptions.Source.Helpers;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Managers.Implementations.Source.Rest
{
    public class ManagerRole : Manager<ManagerRole>, IManagerRole
    {
        protected IRepositoryRole RepositoryRole { get; }

        public ManagerRole(
            IMapper mapper,
            ILogger<ManagerRole> logger,
            IRepositoryRole repositoryRole)
            : base(mapper, logger)
        {
            RepositoryRole = repositoryRole;
        }

        public PagedData<Role> GetRoles(OptionsRole options)
        {
            var (count, roles) = RepositoryRole.GetRoles(options);

            return new PagedData<Role>(Mapper.Map<List<Role>>(roles), count);
        }

        public Role GetRoleById(int id, OptionsRole options)
        {
            var role = RepositoryRole.GetRoleById(id, options) ??
                throw ExceptionHelper.CreateNotFoundException(
                    Messages.Role.NotFoundById(id),
                    Messages.Role.NotFoundPublic);

            return Mapper.Map<Role>(role);
        }

        public Role GetRoleByUserId(int userId, OptionsRole options)
        {
            var role = RepositoryRole.GetRoleByUserId(userId, options) ??
                throw ExceptionHelper.CreateNotFoundException(
                    Messages.Role.NotFoundByUserId(userId),
                    Messages.Role.NotFoundPublic);

            return Mapper.Map<Role>(role);
        }
    }
}