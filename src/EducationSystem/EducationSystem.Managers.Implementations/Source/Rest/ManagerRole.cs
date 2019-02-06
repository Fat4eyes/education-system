using System.Collections.Generic;
using AutoMapper;
using EducationSystem.Exceptions.Source;
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

        public Role GetRoleByUserId(int userId, OptionsRole options)
        {
            var role = RepositoryRole.GetRoleByUserId(userId, options) ??
                  throw new EducationSystemNotFoundException(
                      $"Роль не найдена. Идентификатор пользователя: {userId}.",
                      new EducationSystemPublicException("Роль не найдена."));

            return Mapper.Map<Role>(role);
        }

        public Role GetRoleById(int id, OptionsRole options)
        {
            var role = RepositoryRole.GetById(id) ??
               throw new EducationSystemNotFoundException(
                   $"Роль не найдена. Идентификатор роли: {id}.",
                   new EducationSystemPublicException("Роль не найдена."));

            return Mapper.Map<Role>(role);
        }
    }
}