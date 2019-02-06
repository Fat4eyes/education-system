using AutoMapper;
using EducationSystem.Exceptions.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Managers.Implementations.Source.Rest
{
    public class ManagerStudyProfile : Manager<ManagerStudyProfile>, IManagerStudyProfile
    {
        public IRepositoryStudyProfile RepositoryStudyProfile { get; }

        public ManagerStudyProfile(
            IMapper mapper,
            ILogger<ManagerStudyProfile> logger,
            IRepositoryStudyProfile repositoryStudyProfile)
            : base(mapper, logger)
        {
            RepositoryStudyProfile = repositoryStudyProfile;
        }

        public StudyProfile GetStudyProfileByUserId(int userId, OptionsStudyProfile options)
        {
            var studyProfile = RepositoryStudyProfile.GetStudyProfileByUserId(userId, options) ??
                throw new EducationSystemNotFoundException(
                    $"Профиль обучения не найден. Идентификатор пользователя: {userId}.",
                    new EducationSystemPublicException("Профиль обучения не найден."));

            return Mapper.Map<StudyProfile>(studyProfile);
        }
    }
}