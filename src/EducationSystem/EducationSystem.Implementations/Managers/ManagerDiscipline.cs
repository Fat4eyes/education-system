using System.Threading.Tasks;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Interfaces.Managers;
using EducationSystem.Interfaces.Services;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Rest;

namespace EducationSystem.Implementations.Managers
{
    public sealed class ManagerDiscipline : Manager, IManagerDiscipline
    {
        private readonly IServiceDiscipline _serviceDiscipline;

        public ManagerDiscipline(
            IExecutionContext executionContext,
            IExceptionFactory exceptionFactory,
            IServiceDiscipline serviceDiscipline)
            : base(
                executionContext,
                exceptionFactory)
        {
            _serviceDiscipline = serviceDiscipline;
        }

        public async Task<PagedData<Discipline>> GetDisciplinesAsync(FilterDiscipline filter)
        {
            if (CurrentUser.IsAdmin())
                return await _serviceDiscipline.GetDisciplinesAsync(filter);

            if (CurrentUser.IsLecturer())
                return await _serviceDiscipline.GetLecturerDisciplinesAsync(CurrentUser.Id, filter);

            if (CurrentUser.IsStudent())
                return await _serviceDiscipline.GetStudentDisciplinesAsync(CurrentUser.Id, filter);

            throw ExceptionFactory.NoAccess();
        }

        public async Task<Discipline> GetDisciplineAsync(int id)
        {
            if (CurrentUser.IsAdmin())
                return await _serviceDiscipline.GetDisciplineAsync(id);

            if (CurrentUser.IsLecturer())
                return await _serviceDiscipline.GetLecturerDisciplineAsync(id, CurrentUser.Id);

            if (CurrentUser.IsStudent())
                return await _serviceDiscipline.GetStudentDisciplineAsync(id, CurrentUser.Id);

            throw ExceptionFactory.NoAccess();
        }
    }
}