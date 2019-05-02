using System.Threading.Tasks;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Interfaces.Managers;
using EducationSystem.Interfaces.Services;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
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

        public async Task<PagedData<Discipline>> GetDisciplinesAsync(OptionsDiscipline options, FilterDiscipline filter)
        {
            if (CurrentUser.IsAdmin())
                return await _serviceDiscipline.GetDisciplinesAsync(options, filter);

            if (CurrentUser.IsLecturer())
                return await _serviceDiscipline.GetLecturerDisciplinesAsync(CurrentUser.Id, options, filter);

            if (CurrentUser.IsStudent())
                return await _serviceDiscipline.GetStudentDisciplinesAsync(CurrentUser.Id, options, filter);

            throw ExceptionFactory.NoAccess();
        }

        public async Task<Discipline> GetDisciplineAsync(int id, OptionsDiscipline options)
        {
            if (CurrentUser.IsAdmin())
                return await _serviceDiscipline.GetDisciplineAsync(id, options);

            if (CurrentUser.IsLecturer())
                return await _serviceDiscipline.GetLecturerDisciplineAsync(id, CurrentUser.Id, options);

            if (CurrentUser.IsStudent())
                return await _serviceDiscipline.GetStudentDisciplineAsync(id, CurrentUser.Id, options);

            throw ExceptionFactory.NoAccess();
        }
    }
}