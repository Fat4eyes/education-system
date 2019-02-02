using AutoMapper;
using System.Collections.Generic;
using EducationSystem.Exceptions.Source;
using EducationSystem.Managers.Implementations.Source.Base;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source.Rest;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Managers.Implementations.Source.Rest
{
    /// <summary>
    /// Менеджер по работе с ролями пользователя.
    /// </summary>
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

        /// <inheritdoc />
        public List<Role> GetAll()
        {
            var roles = RepositoryRole.GetAll();

            return Mapper.Map<List<Role>>(roles);
        }

        /// <inheritdoc />
        public Role GetById(int id)
        {
            var role = RepositoryRole.GetById(id) ??
                throw new EducationSystemNotFoundException($"Роль не найдена. Идентификатор: {id}.");

            return Mapper.Map<Role>(role);
        }
    }
}