using AutoMapper;
using EducationSystem.Exceptions.Source;
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
                throw new EducationSystemNotFoundException(
                    $"Пользователь не является студентом. Идентификатор: {studentId}. ",
                    new EducationSystemPublicException("Пользователь не является студентом."));

            var studyPlan = RepositoryStudyPlan.GetStudyPlanByStudentId(studentId, options) ??
                throw new EducationSystemNotFoundException(
                    $"Учебный план не найден. Идентификатор студента (пользователя): {studentId}.",
                    new EducationSystemPublicException("Учебный план не найден."));

            return Mapper.Map<StudyPlan>(studyPlan);
        }
    }
}