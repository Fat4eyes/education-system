using AutoMapper;
using EducationSystem.Exceptions.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Managers.Implementations.Source.Rest
{
    public class ManagerStudyPlan : Manager<ManagerStudyPlan>, IManagerStudyPlan
    {
        protected IRepositoryStudyPlan RepositoryStudyPlan { get; }

        public ManagerStudyPlan(
            IMapper mapper,
            ILogger<ManagerStudyPlan> logger,
            IRepositoryStudyPlan repositoryStudyPlan)
            : base(mapper, logger)
        {
            RepositoryStudyPlan = repositoryStudyPlan;
        }

        public StudyPlan GetStudyPlanByUserId(int userId, OptionsStudyPlan options)
        {
            var studyPlan = RepositoryStudyPlan.GetStudyPlanByUserId(userId, options) ??
                throw new EducationSystemNotFoundException(
                    $"Учебный план не найден. Идентификатор пользователя: {userId}.",
                    new EducationSystemPublicException("Учебный план не найден."));

            return Mapper.Map<StudyPlan>(studyPlan);
        }
    }
}