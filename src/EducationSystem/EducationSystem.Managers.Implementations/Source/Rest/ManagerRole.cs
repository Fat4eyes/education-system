using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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

        public PagedData<Role> GetRoles(OptionsRole options, Filter filter)
        {
            var (count, roles) = RepositoryRole.GetRoles(filter);

            return new PagedData<Role>(roles.Select(Map).ToList(), count);
        }

        public Role GetRoleById(int id, OptionsRole options)
        {
            var role = RepositoryRole.GetById(id) ??
                throw ExceptionHelper.CreateNotFoundException(
                    Messages.Role.NotFoundById(id),
                    Messages.Role.NotFoundPublic);

            return Mapper.Map<Role>(Map(role));
        }

        public Role GetRoleByUserId(int userId, OptionsRole options)
        {
            var role = RepositoryRole.GetRoleByUserId(userId) ??
                throw ExceptionHelper.CreateNotFoundException(
                    Messages.Role.NotFoundByUserId(userId),
                    Messages.Role.NotFoundPublic);

            return Mapper.Map<Role>(Map(role));
        }

        private Role Map(DatabaseRole role)
        {
            return Mapper.Map<DatabaseRole, Role>(role);
        }
    }
}