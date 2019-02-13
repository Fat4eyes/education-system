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
    public class ManagerStudyProfile : Manager<ManagerStudyProfile>, IManagerStudyProfile
    {
        protected IUserHelper UserHelper { get; }
        protected IRepositoryStudyProfile RepositoryStudyProfile { get; }

        public ManagerStudyProfile(
            IMapper mapper,
            ILogger<ManagerStudyProfile> logger,
            IUserHelper userHelper,
            IRepositoryStudyProfile repositoryStudyProfile)
            : base(mapper, logger)
        {
            UserHelper = userHelper;
            RepositoryStudyProfile = repositoryStudyProfile;
        }

        public StudyProfile GetStudyProfileByStudentId(int studentId, OptionsStudyProfile options)
        {
            if (!UserHelper.IsStudent(studentId))
                throw new EducationSystemNotFoundException(
                    $"Пользователь не является студентом. Идентификатор: {studentId}. ",
                    new EducationSystemPublicException("Пользователь не является студентом."));

            var studyProfile = RepositoryStudyProfile.GetStudyProfileByStudentId(studentId, options) ??
                throw new EducationSystemNotFoundException(
                    $"Профиль обучения не найден. Идентификатор пользователя (студента): {studentId}.",
                    new EducationSystemPublicException("Профиль обучения не найден."));

            return Mapper.Map<StudyProfile>(studyProfile);
        }
    }
}