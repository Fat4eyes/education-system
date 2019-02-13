using System.Collections.Generic;
using AutoMapper;
using EducationSystem.Exceptions.Source;
using EducationSystem.Helpers.Interfaces.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Managers.Implementations.Source.Rest
{
    public class ManagerGroup : Manager<ManagerGroup>, IManagerGroup
    {
        protected IUserHelper UserHelper { get; }
        protected IRepositoryGroup RepositoryGroup { get; }

        public ManagerGroup(
            IMapper mapper,
            ILogger<ManagerGroup> logger,
            IUserHelper userHelper,
            IRepositoryGroup repositoryGroup)
            : base(mapper, logger)
        {
            UserHelper = userHelper;
            RepositoryGroup = repositoryGroup;
        }

        public PagedData<Group> GetGroups(OptionsGroup options)
        {
            var (count, groups) = RepositoryGroup.GetGroups(options);

            return new PagedData<Group>(Mapper.Map<List<Group>>(groups), count);
        }

        public Group GetGroupById(int id, OptionsGroup options)
        {
            var group = RepositoryGroup.GetGroupById(id, options) ??
                throw new EducationSystemNotFoundException(
                    $"Группа не найдена. Идентификатор группы: {id}.",
                    new EducationSystemPublicException("Группа не найдена."));

            return Mapper.Map<Group>(group);
        }

        public Group GetGroupByStudentId(int studentId, OptionsGroup options)
        {
            if (!UserHelper.IsStudent(studentId))
                throw new EducationSystemNotFoundException(
                    $"Пользователь не является студентом. Идентификатор: {studentId}. ",
                    new EducationSystemPublicException("Пользователь не является студентом."));

            var group = RepositoryGroup.GetGroupByStudentId(studentId, options) ??
                throw new EducationSystemNotFoundException(
                    $"Группа не найдена. Идентификатор студента (пользователя): {studentId}.",
                    new EducationSystemPublicException("Группа не найдена."));

            return Mapper.Map<Group>(group);
        }
    }
}