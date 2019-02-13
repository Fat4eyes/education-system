using AutoMapper;
using EducationSystem.Constants.Source;
using EducationSystem.Exceptions.Source.Helpers;
using EducationSystem.Helpers.Interfaces.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Managers.Implementations.Source.Rest
{
    public class ManagerStudyPlan : Manager<ManagerStudyPlan>, IManagerStudyPlan
    {
        protected IUserHelper UserHelper { get; }
        protected IRepositoryStudyPlan RepositoryStudyPlan { get; }

        public ManagerStudyPlan(
            IMapper mapper,
            ILogger<ManagerStudyPlan> logger,
            IUserHelper userHelper,
            IRepositoryStudyPlan repositoryStudyPlan)
            : base(mapper, logger)
        {
            UserHelper = userHelper;
            RepositoryStudyPlan = repositoryStudyPlan;
        }

        public StudyPlan GetStudyPlanByStudentId(int studentId, OptionsStudyPlan options)
        {
            if (!UserHelper.IsStudent(studentId))
                throw ExceptionHelper.CreateException(
                    Messages.User.NotStudent(studentId),
                    Messages.User.NotStudentPublic);

            var studyPlan = RepositoryStudyPlan.GetStudyPlanByStudentId(studentId, options) ??
                throw ExceptionHelper.CreateNotFoundException(
                    Messages.StudyPlan.NotFoundByStuentId(studentId),
                    Messages.StudyPlan.NotFoundPublic);

            return Mapper.Map<StudyPlan>(studyPlan);
        }
    }
}