using System.Linq;
using AutoMapper;
using EducationSystem.Database.Models.Source;
using EducationSystem.Exceptions.Source.Helpers;
using EducationSystem.Helpers.Interfaces.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Managers.Implementations.Source.Rest
{
    public sealed class ManagerGroup : Manager<ManagerGroup>, IManagerGroup
    {
        private readonly IUserHelper _userHelper;
        private readonly IRepositoryGroup _repositoryGroup;

        public ManagerGroup(
            IMapper mapper,
            ILogger<ManagerGroup> logger,
            IUserHelper userHelper,
            IRepositoryGroup repositoryGroup)
            : base(mapper, logger)
        {
            _userHelper = userHelper;
            _repositoryGroup = repositoryGroup;
        }

        public PagedData<Group> GetGroups(OptionsGroup options, FilterGroup filter)
        {
            var (count, groups) = _repositoryGroup.GetGroups(filter);

            return new PagedData<Group>(groups.Select(x => Map(x, options)).ToList(), count);
        }

        public Group GetGroupById(int id, OptionsGroup options)
        {
            var group = _repositoryGroup.GetById(id) ??
                throw ExceptionHelper.CreateNotFoundException(
                    $"Группа не найдена. Идентификатор группы: {id}.",
                    $"Группа не найдена.");

            return Map(group, options);
        }

        public Group GetGroupByStudentId(int studentId, OptionsGroup options)
        {
            _userHelper.CheckRoleStudent(studentId);

            var group = _repositoryGroup.GetGroupByStudentId(studentId) ??
                throw ExceptionHelper.CreateNotFoundException(
                    $"Группа не найдена. Идентификатор студента (пользователя): {studentId}.",
                    $"Группа не найдена.");

            return Map(group, options);
        }

        private Group Map(DatabaseGroup group, OptionsGroup options)
        {
            return Mapper.Map<DatabaseGroup, Group>(group, x =>
            {
                x.AfterMap((s, d) =>
                {
                    if (options.WithStudyPlan)
                        d.StudyPlan = Mapper.Map<StudyPlan>(s.StudyPlan);
                });
            });
        }
    }
}