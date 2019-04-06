using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Exceptions.Helpers;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Managers;
using EducationSystem.Models.Options;
using EducationSystem.Models.Rest;
using EducationSystem.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Managers
{
    public sealed class ManagerStudyPlan : Manager<ManagerStudyPlan>, IManagerStudyPlan
    {
        private readonly IHelperUserRole _helperUserRole;
        private readonly IRepositoryStudyPlan _repositoryStudyPlan;

        public ManagerStudyPlan(
            IMapper mapper,
            ILogger<ManagerStudyPlan> logger,
            IHelperUserRole helperUserRole,
            IRepositoryStudyPlan repositoryStudyPlan)
            : base(mapper, logger)
        {
            _helperUserRole = helperUserRole;
            _repositoryStudyPlan = repositoryStudyPlan;
        }

        public StudyPlan GetStudyPlanByStudentId(int studentId, OptionsStudyPlan options)
        {
            _helperUserRole.CheckRoleStudent(studentId);

            var studyPlan = _repositoryStudyPlan.GetStudyPlanByStudentId(studentId) ??
                throw ExceptionHelper.CreateNotFoundException(
                    $"Учебный план не найден. Идентификатор студента: {studentId}.",
                    $"Учебный план не найден.");

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