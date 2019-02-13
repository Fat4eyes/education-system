using System.Collections.Generic;
using AutoMapper;
using EducationSystem.Constants.Source;
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
                    string.Format(Messages.Group.NotFoundById, id),
                    new EducationSystemPublicException(Messages.Group.NotFoundPublic));

            return Mapper.Map<Group>(group);
        }

        public Group GetGroupByStudentId(int studentId, OptionsGroup options)
        {
            if (!UserHelper.IsStudent(studentId))
                throw new EducationSystemNotFoundException(
                    string.Format(Messages.User.NotStudent, studentId),
                    new EducationSystemPublicException(Messages.User.NotStudentPublic));

            var group = RepositoryGroup.GetGroupByStudentId(studentId, options) ??
                throw new EducationSystemNotFoundException(
                    string.Format(Messages.Group.NotFoundByStudentId, studentId),
                    new EducationSystemPublicException(Messages.Group.NotFoundPublic));

            return Mapper.Map<Group>(group);
        }
    }
}