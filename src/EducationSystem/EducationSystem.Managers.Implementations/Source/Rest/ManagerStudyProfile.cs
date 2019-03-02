using AutoMapper;
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
    public sealed class ManagerStudyProfile : Manager<ManagerStudyProfile>, IManagerStudyProfile
    {
        private readonly IUserHelper _userHelper;
        private readonly IRepositoryStudyProfile _repositoryStudyProfile;

        public ManagerStudyProfile(
            IMapper mapper,
            ILogger<ManagerStudyProfile> logger,
            IUserHelper userHelper,
            IRepositoryStudyProfile repositoryStudyProfile)
            : base(mapper, logger)
        {
            _userHelper = userHelper;
            _repositoryStudyProfile = repositoryStudyProfile;
        }

        public StudyProfile GetStudyProfileByStudentId(int studentId, OptionsStudyProfile options)
        {
            _userHelper.CheckRoleStudent(studentId);

            var studyProfile = _repositoryStudyProfile.GetStudyProfileByStudentId(studentId) ??
                throw ExceptionHelper.CreateNotFoundException(
                    $"Профиль обучения не найден. Идентификатор студента: {studentId}.",
                    $"Профиль обучения не найден.");

            return Map(studyProfile, options);
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