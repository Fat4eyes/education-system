using System.Linq;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Exceptions.Helpers;
using EducationSystem.Interfaces.Managers.Rest;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.Models.Rest;
using EducationSystem.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Managers.Rest
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
                    $"Роль не найдена. Идентификатор роли: {id}.",
                    $"Роль не найдена.");

            return Map(role);
        }

        public Role GetRoleByUserId(int userId, OptionsRole options)
        {
            var role = _repositoryRole.GetRoleByUserId(userId) ??
                throw ExceptionHelper.CreateNotFoundException(
                    $"Роль не найдена. Идентификатор пользователя: {userId}.",
                    $"Роль не найдена.");

            return Mapper.Map<Role>(Map(role));
        }

        private Role Map(DatabaseRole role)
        {
            return Mapper.Map<DatabaseRole, Role>(role);
        }
    }
}