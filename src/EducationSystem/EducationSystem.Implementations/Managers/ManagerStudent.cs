using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Exceptions.Helpers;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Managers;
using EducationSystem.Models.Rest;
using EducationSystem.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Managers
{
    public sealed class ManagerStudent : Manager<ManagerStudent>, IManagerStudent
    {
        private readonly IHelperUserRole _helperUserRole;
        private readonly IRepositoryStudent _repositoryStudent;

        public ManagerStudent(
            IMapper mapper,
            ILogger<ManagerStudent> logger,
            IHelperUserRole helperUserRole,
            IRepositoryStudent repositoryStudent)
            : base(mapper, logger)
        {
            _helperUserRole = helperUserRole;
            _repositoryStudent = repositoryStudent;
        }

        public async Task<Student> GetStudent(int id)
        {
            _helperUserRole.CheckRoleStudent(id);

            var student = await _repositoryStudent.GetById(id) ??
                throw ExceptionHelper.CreateNotFoundException(
                    $"Студент не найден. Идентификатор студента: {id}.",
                    $"Студент не найден.");

            return Mapper.Map<Student>(student);
        }
    }
}