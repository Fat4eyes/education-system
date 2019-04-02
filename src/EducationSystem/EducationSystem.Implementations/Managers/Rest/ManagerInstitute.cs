using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Exceptions.Helpers;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Managers.Rest;
using EducationSystem.Models.Options;
using EducationSystem.Models.Rest;
using EducationSystem.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Managers.Rest
{
    public sealed class ManagerInstitute : Manager<ManagerInstitute>, IManagerInstitute
    {
        private readonly IHelperUserRole _helperUserRole;
        private readonly IRepositoryInstitute _repositoryInstitute;

        public ManagerInstitute(
            IMapper mapper,
            ILogger<ManagerInstitute> logger,
            IHelperUserRole helperUserRole,
            IRepositoryInstitute repositoryInstitute)
            : base(mapper, logger)
        {
            _helperUserRole = helperUserRole;
            _repositoryInstitute = repositoryInstitute;
        }

        public Institute GetInstituteByStudentId(int studentId, OptionsInstitute options)
        {
            _helperUserRole.CheckRoleStudent(studentId);

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