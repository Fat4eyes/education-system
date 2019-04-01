using AutoMapper;
using EducationSystem.Database.Models.Source;
using EducationSystem.Exceptions.Source.Helpers;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Managers.Rest;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Managers.Rest
{
    public sealed class ManagerInstitute : Manager<ManagerInstitute>, IManagerInstitute
    {
        private readonly IHelperUser _helperUser;
        private readonly IRepositoryInstitute _repositoryInstitute;

        public ManagerInstitute(
            IMapper mapper,
            ILogger<ManagerInstitute> logger,
            IHelperUser helperUser,
            IRepositoryInstitute repositoryInstitute)
            : base(mapper, logger)
        {
            _helperUser = helperUser;
            _repositoryInstitute = repositoryInstitute;
        }

        public Institute GetInstituteByStudentId(int studentId, OptionsInstitute options)
        {
            _helperUser.CheckRoleStudent(studentId);

            var institute = _repositoryInstitute.GetInstituteByStudentId(studentId) ??
                throw ExceptionHelper.CreateNotFoundException(
                    $"Институт не найден. Идентификатор студента: {studentId}.",
                    $"Институт не найден.");

            return Map(institute);
        }

        private Institute Map(DatabaseInstitute institute)
        {
            return Mapper.Map<DatabaseInstitute, Institute>(institute);
        }
    }
}