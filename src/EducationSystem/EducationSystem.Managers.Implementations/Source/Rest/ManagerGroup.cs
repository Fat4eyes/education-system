using AutoMapper;
using System.Collections.Generic;
using EducationSystem.Exceptions.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Managers.Implementations.Source.Rest
{
    /// <summary>
    /// Менеджер по работе с группами.
    /// </summary>
    public class ManagerGroup : Manager<ManagerGroup>, IManagerGroup
    {
        protected IRepositoryGroup RepositoryGroup { get; }

        public ManagerGroup(
            IMapper mapper,
            ILogger<ManagerGroup> logger,
            IRepositoryGroup repositoryGroup)
            : base(mapper, logger)
        {
            RepositoryGroup = repositoryGroup;
        }

        /// <inheritdoc />
        public List<Group> GetAll()
        {
            var groups = RepositoryGroup.GetAll();

            return Mapper.Map<List<Group>>(groups);
        }

        /// <inheritdoc />
        public Group GetById(int id)
        {
            var group = RepositoryGroup.GetById(id) ??
                throw new EducationSystemNotFoundException($"Группа не найдена. Идентификатор: {id}.");

            return Mapper.Map<Group>(group);
        }
    }
}