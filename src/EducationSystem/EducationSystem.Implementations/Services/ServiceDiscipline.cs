using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Implementations.Specifications;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Interfaces.Services;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Rest;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Services
{
    public sealed class ServiceDiscipline : Service<ServiceDiscipline>, IServiceDiscipline
    {
        private readonly IHelperUserRole _helperUserRole;
        private readonly IExceptionFactory _exceptionFactory;
        private readonly IRepository<DatabaseDiscipline> _repositoryDiscipline;

        public ServiceDiscipline(
            IMapper mapper,
            ILogger<ServiceDiscipline> logger,
            IHelperUserRole helperUserRole,
            IExceptionFactory exceptionFactory,
            IRepository<DatabaseDiscipline> repositoryDiscipline)
            : base(mapper, logger)
        {
            _helperUserRole = helperUserRole;
            _exceptionFactory = exceptionFactory;
            _repositoryDiscipline = repositoryDiscipline;
        }

        public async Task<PagedData<Discipline>> GetDisciplinesAsync(FilterDiscipline filter)
        {
            var specification = new DisciplinesByName(filter.Name);

            var (count, disciplines) = await _repositoryDiscipline.FindPaginatedAsync(specification, filter);

            return new PagedData<Discipline>(Mapper.Map<List<Discipline>>(disciplines), count);
        }

        public async Task<PagedData<Discipline>> GetStudentDisciplinesAsync(int studentId, FilterDiscipline filter)
        {
            await _helperUserRole.CheckRoleStudentAsync(studentId);

            var specification =
                new DisciplinesByName(filter.Name) &
                new DisciplinesByStudentId(studentId) &
                new DisciplinesForStudents();

            var (count, disciplines) = await _repositoryDiscipline.FindPaginatedAsync(specification, filter);

            return new PagedData<Discipline>(Mapper.Map<List<Discipline>>(disciplines), count);
        }

        public async Task<PagedData<Discipline>> GetLecturerDisciplinesAsync(int lecturerId, FilterDiscipline filter)
        {
            await _helperUserRole.CheckRoleLecturerAsync(lecturerId);

            var specification =
                new DisciplinesByName(filter.Name) &
                new DisciplinesByLecturerId(lecturerId);

            var (count, disciplines) = await _repositoryDiscipline.FindPaginatedAsync(specification, filter);

            return new PagedData<Discipline>(Mapper.Map<List<Discipline>>(disciplines), count);
        }

        public async Task<Discipline> GetDisciplineAsync(int id)
        {
            var discipline = await _repositoryDiscipline.FindFirstAsync(new DisciplinesById(id)) ??
                throw _exceptionFactory.NotFound<DatabaseDiscipline>(id);

            return Mapper.Map<Discipline>(discipline);
        }

        public async Task<Discipline> GetStudentDisciplineAsync(int id, int studentId)
        {
            await _helperUserRole.CheckRoleStudentAsync(studentId);

            var specification =
                new DisciplinesById(id) &
                new DisciplinesByStudentId(studentId);

            var discipline = await _repositoryDiscipline.FindFirstAsync(specification) ??
                throw _exceptionFactory.NotFound<DatabaseDiscipline>(id);

            return Mapper.Map<Discipline>(discipline);
        }

        public async Task<Discipline> GetLecturerDisciplineAsync(int id, int lecturerId)
        {
            await _helperUserRole.CheckRoleLecturerAsync(lecturerId);

            var specification =
                new DisciplinesById(id) &
                new DisciplinesByLecturerId(lecturerId);

            var discipline = await _repositoryDiscipline.FindFirstAsync(specification) ??
                throw _exceptionFactory.NotFound<DatabaseDiscipline>(id);

            return Mapper.Map<Discipline>(discipline);
        }
    }
}