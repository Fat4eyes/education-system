using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Services;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Rest;
using EducationSystem.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Services
{
    public sealed class ServiceDiscipline : Service<ServiceDiscipline>, IServiceDiscipline
    {
        private readonly IHelperUserRole _helperUserRole;
        private readonly IExceptionFactory _exceptionFactory;
        private readonly IRepositoryDiscipline _repositoryDiscipline;

        public ServiceDiscipline(
            IMapper mapper,
            ILogger<ServiceDiscipline> logger,
            IHelperUserRole helperUserRole,
            IExceptionFactory exceptionFactory,
            IRepositoryDiscipline repositoryDiscipline)
            : base(mapper, logger)
        {
            _helperUserRole = helperUserRole;
            _exceptionFactory = exceptionFactory;
            _repositoryDiscipline = repositoryDiscipline;
        }

        public async Task<PagedData<Discipline>> GetDisciplinesAsync(FilterDiscipline filter)
        {
            var (count, disciplines) = await _repositoryDiscipline.GetDisciplinesAsync(filter);

            return new PagedData<Discipline>(Mapper.Map<List<Discipline>>(disciplines), count);
        }

        public async Task<PagedData<Discipline>> GetStudentDisciplinesAsync(int studentId, FilterDiscipline filter)
        {
            await _helperUserRole.CheckRoleStudentAsync(studentId);

            var (count, disciplines) = await _repositoryDiscipline.GetStudentDisciplinesAsync(studentId, filter);

            return new PagedData<Discipline>(Mapper.Map<List<Discipline>>(disciplines), count);
        }

        public async Task<PagedData<Discipline>> GetLecturerDisciplinesAsync(int lecturerId, FilterDiscipline filter)
        {
            await _helperUserRole.CheckRoleLecturerAsync(lecturerId);

            var (count, disciplines) = await _repositoryDiscipline.GetLecturerDisciplinesAsync(lecturerId, filter);

            return new PagedData<Discipline>(Mapper.Map<List<Discipline>>(disciplines), count);
        }

        public async Task<Discipline> GetDisciplineAsync(int id)
        {
            var discipline = await _repositoryDiscipline.GetByIdAsync(id) ??
                throw _exceptionFactory.NotFound<DatabaseDiscipline>(id);

            return Mapper.Map<Discipline>(discipline);
        }

        public async Task<Discipline> GetStudentDisciplineAsync(int id, int studentId)
        {
            await _helperUserRole.CheckRoleStudentAsync(studentId);

            var discipline = await _repositoryDiscipline.GetStudentDisciplineAsync(id, studentId) ??
                throw _exceptionFactory.NotFound<DatabaseDiscipline>(id);

            return Mapper.Map<Discipline>(discipline);
        }

        public async Task<Discipline> GetLecturerDisciplineAsync(int id, int lecturerId)
        {
            await _helperUserRole.CheckRoleLecturerAsync(lecturerId);

            var discipline = await _repositoryDiscipline.GetLecturerDisciplineAsync(id, lecturerId) ??
                throw _exceptionFactory.NotFound<DatabaseDiscipline>(id);

            return Mapper.Map<Discipline>(discipline);
        }
    }
}