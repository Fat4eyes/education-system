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

        public async Task<Institute> GetInstituteByStudentId(int studentId, OptionsInstitute options)
        {
            _helperUserRole.CheckRoleStudent(studentId);

            var institute = await _repositoryInstitute.GetInstituteByStudentId(studentId) ??
                throw ExceptionHelper.CreateNotFoundException(
                    $"Институт не найден. Идентификатор студента: {studentId}.",
                    $"Институт не найден.");

            return Mapper.Map<DatabaseInstitute, Institute>(institute);
        }
    }
}