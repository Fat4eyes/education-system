using AutoMapper;
using EducationSystem.Constants.Source;
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
                    string.Format(Messages.User.NotStudent, studentId),
                    new EducationSystemPublicException(Messages.User.NotStudentPublic));

            var studyProfile = RepositoryStudyProfile.GetStudyProfileByStudentId(studentId, options) ??
                throw new EducationSystemNotFoundException(
                    string.Format(Messages.StudyProfile.NotFoundByStuentId, studentId),
                    new EducationSystemPublicException(Messages.StudyProfile.NotFoundPublic));

            return Mapper.Map<StudyProfile>(studyProfile);
        }
    }
}