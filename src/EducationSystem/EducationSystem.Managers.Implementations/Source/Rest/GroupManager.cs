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
    /// Менеджер по работе с группами.
    /// </summary>
    public class GroupManager : Manager<GroupManager>, IGroupManager
    {
        protected IGroupRepository GroupRepository { get; }

        public GroupManager(
            IMapper mapper,
            ILogger<GroupManager> logger,
            IGroupRepository groupRepository)
            : base(mapper, logger)
        {
            GroupRepository = groupRepository;
        }

        /// <inheritdoc />
        public List<Group> GetAll()
        {
            var groups = GroupRepository.GetAll();

            return Mapper.Map<List<Group>>(groups);
        }

        /// <inheritdoc />
        public Group GetById(int id)
        {
            var group = GroupRepository.GetById(id) ??
                throw new EducationSystemNotFoundException($"Группа не найдена. Идентификатор: {id}.");

            return Mapper.Map<Group>(group);
        }
    }
}