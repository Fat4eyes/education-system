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
    public class ManagerInstitute : Manager<ManagerInstitute>, IManagerInstitute
    {
        protected IUserHelper UserHelper { get; }
        protected IRepositoryInstitute RepositoryInstitute { get; }

        public ManagerInstitute(
            IMapper mapper,
            ILogger<ManagerInstitute> logger,
            IUserHelper userHelper,
            IRepositoryInstitute repositoryInstitute)
            : base(mapper, logger)
        {
            UserHelper = userHelper;
            RepositoryInstitute = repositoryInstitute;
        }

        public Institute GetInstituteByStudentId(int studentId, OptionsInstitute options)
        {
            if (!UserHelper.IsStudent(studentId))
                throw new EducationSystemNotFoundException(
                    string.Format(Messages.User.NotStudent, studentId),
                    new EducationSystemPublicException(Messages.User.NotStudentPublic));

            var institute = RepositoryInstitute.GetInstituteByStudentId(studentId, options) ??
                throw new EducationSystemNotFoundException(
                    string.Format(Messages.Institute.NotFoundByStuentId, studentId),
                    new EducationSystemPublicException(Messages.Institute.NotFoundPublic));

            return Mapper.Map<Institute>(institute);
        }
    }
}