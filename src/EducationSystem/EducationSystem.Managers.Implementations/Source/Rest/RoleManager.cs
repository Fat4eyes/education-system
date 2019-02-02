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
    public class RoleManager : Manager<RoleManager>, IRoleManager
    {
        protected IRoleRepository RoleRepository { get; }

        public RoleManager(
            IMapper mapper,
            ILogger<RoleManager> logger,
            IRoleRepository roleRepository)
            : base(mapper, logger)
        {
            RoleRepository = roleRepository;
        }

        /// <inheritdoc />
        public List<Role> GetAll()
        {
            var roles = RoleRepository.GetAll();

            return Mapper.Map<List<Role>>(roles);
        }

        /// <inheritdoc />
        public Role GetById(int id)
        {
            var role = RoleRepository.GetById(id) ??
                throw new EducationSystemNotFoundException($"Роль не найдена. Идентификатор: {id}.");

            return Mapper.Map<Role>(role);
        }
    }
}