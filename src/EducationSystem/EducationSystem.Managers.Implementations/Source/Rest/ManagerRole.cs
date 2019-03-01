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
    public sealed class ManagerRole : Manager<ManagerRole>, IManagerRole
    {
        private readonly IRepositoryRole _repositoryRole;

        public ManagerRole(
            IMapper mapper,
            ILogger<ManagerRole> logger,
            IRepositoryRole repositoryRole)
            : base(mapper, logger)
        {
            _repositoryRole = repositoryRole;
        }

        public PagedData<Role> GetRoles(OptionsRole options, FilterRole filter)
        {
            var (count, roles) = _repositoryRole.GetRoles(filter);

            return new PagedData<Role>(roles.Select(Map).ToList(), count);
        }

        public Role GetRoleById(int id, OptionsRole options)
        {
            var role = _repositoryRole.GetById(id) ??
                throw ExceptionHelper.CreateNotFoundException(
                    Messages.Role.NotFoundById(id),
                    Messages.Role.NotFoundPublic);

            return Map(role);
        }

        public Role GetRoleByUserId(int userId, OptionsRole options)
        {
            var role = _repositoryRole.GetRoleByUserId(userId) ??
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