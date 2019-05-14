using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Helpers;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Interfaces.Services;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Rest;
using EducationSystem.Specifications.Disciplines;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Services
{
    public sealed class ServiceDiscipline : Service<ServiceDiscipline>, IServiceDiscipline
    {
        private readonly IRepository<DatabaseDiscipline> _repositoryDiscipline;

        public ServiceDiscipline(
            IMapper mapper,
            ILogger<ServiceDiscipline> logger,
            IExecutionContext executionContext,
            IRepository<DatabaseDiscipline> repositoryDiscipline)
            : base(
                mapper,
                logger,
                executionContext)
        {
            _repositoryDiscipline = repositoryDiscipline;
        }

        public async Task<PagedData<Discipline>> GetDisciplinesAsync(FilterDiscipline filter)
        {
            var user = await ExecutionContext.GetCurrentUserAsync();

            if (user.IsAdmin())
            {
                var specification = new DisciplinesByName(filter.Name);

                var (count, disciplines) = await _repositoryDiscipline.FindPaginatedAsync(specification, filter);

                return new PagedData<Discipline>(Mapper.Map<List<Discipline>>(disciplines), count);
            }

            if (user.IsLecturer())
            {
                var specification =
                    new DisciplinesByName(filter.Name) &
                    new DisciplinesByLecturerId(user.Id);

                var (count, disciplines) = await _repositoryDiscipline.FindPaginatedAsync(specification, filter);

                return new PagedData<Discipline>(Mapper.Map<List<Discipline>>(disciplines), count);
            }

            if (user.IsStudent())
            {
                var specification =
                    new DisciplinesByName(filter.Name) &
                    new DisciplinesByStudentId(user.Id) &
                    new DisciplinesForStudents();

                var (count, disciplines) = await _repositoryDiscipline.FindPaginatedAsync(specification, filter);

                return new PagedData<Discipline>(Mapper.Map<List<Discipline>>(disciplines), count);
            }

            throw ExceptionHelper.NoAccess();
        }

        public async Task<Discipline> GetDisciplineAsync(int id)
        {
            var user = await ExecutionContext.GetCurrentUserAsync();

            if (user.IsAdmin())
            {
                var discipline = await _repositoryDiscipline.FindFirstAsync(new DisciplinesById(id)) ??
                    throw ExceptionHelper.NotFound<DatabaseDiscipline>(id);

                return Mapper.Map<Discipline>(discipline);
            }

            if (user.IsLecturer())
            {
                var discipline = await _repositoryDiscipline.FindFirstAsync(new DisciplinesById(id)) ??
                    throw ExceptionHelper.NotFound<DatabaseDiscipline>(id);

                if (new DisciplinesByLecturerId(user.Id).IsSatisfiedBy(discipline) == false)
                    throw ExceptionHelper.NoAccess();

                return Mapper.Map<Discipline>(discipline);
            }

            if (user.IsStudent())
            {
                var discipline = await _repositoryDiscipline.FindFirstAsync(new DisciplinesById(id)) ??
                    throw ExceptionHelper.NotFound<DatabaseDiscipline>(id);

                if (new DisciplinesByStudentId(user.Id).IsSatisfiedBy(discipline) == false)
                    throw ExceptionHelper.NoAccess();

                return Mapper.Map<Discipline>(discipline);
            }

            throw ExceptionHelper.NoAccess();
        }
    }
}