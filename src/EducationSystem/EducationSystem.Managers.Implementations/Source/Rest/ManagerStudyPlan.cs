using AutoMapper;
using EducationSystem.Constants.Source;
using EducationSystem.Database.Models.Source;
using EducationSystem.Exceptions.Source.Helpers;
using EducationSystem.Helpers.Interfaces.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Managers.Implementations.Source.Rest
{
    public sealed class ManagerStudyPlan : Manager<ManagerStudyPlan>, IManagerStudyPlan
    {
        private readonly IUserHelper _userHelper;
        private readonly IRepositoryStudyPlan _repositoryStudyPlan;

        public ManagerStudyPlan(
            IMapper mapper,
            ILogger<ManagerStudyPlan> logger,
            IUserHelper userHelper,
            IRepositoryStudyPlan repositoryStudyPlan)
            : base(mapper, logger)
        {
            _userHelper = userHelper;
            _repositoryStudyPlan = repositoryStudyPlan;
        }

        public StudyPlan GetStudyPlanByStudentId(int studentId, OptionsStudyPlan options)
        {
            if (!_userHelper.IsStudent(studentId))
                throw ExceptionHelper.CreateException(
                    Messages.User.NotStudent(studentId),
                    Messages.User.NotStudentPublic);

            var studyPlan = _repositoryStudyPlan.GetStudyPlanByStudentId(studentId) ??
                throw ExceptionHelper.CreateNotFoundException(
                    Messages.StudyPlan.NotFoundByStuentId(studentId),
                    Messages.StudyPlan.NotFoundPublic);

            return Map(studyPlan, options);
        }

        private StudyPlan Map(DatabaseStudyPlan studyPlan, OptionsStudyPlan options)
        {
            return Mapper.Map<DatabaseStudyPlan, StudyPlan>(studyPlan, x =>
            {
                x.AfterMap((s, d) =>
                {
                    if (options.WithStudyProfile)
                        d.StudyProfile = Mapper.Map<StudyProfile>(s.StudyProfile);
                });
            });
        }
    }
}