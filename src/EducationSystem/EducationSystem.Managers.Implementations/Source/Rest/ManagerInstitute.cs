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
    public sealed class ManagerInstitute : Manager<ManagerInstitute>, IManagerInstitute
    {
        private readonly IUserHelper _userHelper;
        private readonly IRepositoryInstitute _repositoryInstitute;

        public ManagerInstitute(
            IMapper mapper,
            ILogger<ManagerInstitute> logger,
            IUserHelper userHelper,
            IRepositoryInstitute repositoryInstitute)
            : base(mapper, logger)
        {
            _userHelper = userHelper;
            _repositoryInstitute = repositoryInstitute;
        }

        public Institute GetInstituteByStudentId(int studentId, OptionsInstitute options)
        {
            _userHelper.CheckRoleStudent(studentId);

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