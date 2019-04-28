using System.Threading.Tasks;
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
    public sealed class ManagerStudyProfile : Manager<ManagerStudyProfile>, IManagerStudyProfile
    {
        private readonly IHelperUserRole _helperUserRole;
        private readonly IRepositoryStudyProfile _repositoryStudyProfile;

        public ManagerStudyProfile(
            IMapper mapper,
            ILogger<ManagerStudyProfile> logger,
            IHelperUserRole helperUserRole,
            IRepositoryStudyProfile repositoryStudyProfile)
            : base(mapper, logger)
        {
            _helperUserRole = helperUserRole;
            _repositoryStudyProfile = repositoryStudyProfile;
        }

        public Task<StudyProfile> GetStudyProfileByStudentId(int studentId, OptionsStudyProfile options)
        {
            _helperUserRole.CheckRoleStudent(studentId);

            var studyProfile = _repositoryStudyProfile.GetStudyProfileByStudentId(studentId) ??
                throw ExceptionHelper.CreateNotFoundException(
                    $"Профиль обучения не найден. Идентификатор студента: {studentId}.",
                    $"Профиль обучения не найден.");

            return Task.FromResult(Map(studyProfile, options));
        }

        private StudyProfile Map(DatabaseStudyProfile studyProfile, OptionsStudyProfile options)
        {
            return Mapper.Map<DatabaseStudyProfile, StudyProfile>(studyProfile, x =>
            {
                x.AfterMap((s, d) =>
                {
                    if (options.WithInstitute)
                        d.Institute = Mapper.Map<Institute>(s.Institute);
                });
            });
        }
    }
}