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
            if (CurrentUser.IsAdmin())
            {
                var specification = new DisciplinesByName(filter.Name);

                var (count, disciplines) = await _repositoryDiscipline.FindPaginatedAsync(specification, filter);

                return new PagedData<Discipline>(Mapper.Map<List<Discipline>>(disciplines), count);
            }

            if (CurrentUser.IsLecturer())
            {
                var specification =
                    new DisciplinesByName(filter.Name) &
                    new DisciplinesByLecturerId(CurrentUser.Id);

                var (count, disciplines) = await _repositoryDiscipline.FindPaginatedAsync(specification, filter);

                return new PagedData<Discipline>(Mapper.Map<List<Discipline>>(disciplines), count);
            }

            if (CurrentUser.IsStudent())
            {
                var specification =
                    new DisciplinesByName(filter.Name) &
                    new DisciplinesByStudentId(CurrentUser.Id) &
                    new DisciplinesForStudents();

                var (count, disciplines) = await _repositoryDiscipline.FindPaginatedAsync(specification, filter);

                return new PagedData<Discipline>(Mapper.Map<List<Discipline>>(disciplines), count);
            }

            throw ExceptionHelper.NoAccess();
        }

        public async Task<Discipline> GetDisciplineAsync(int id)
        {
            if (CurrentUser.IsAdmin())
            {
                var discipline = await _repositoryDiscipline.FindFirstAsync(new DisciplinesById(id)) ??
                    throw ExceptionHelper.NotFound<DatabaseDiscipline>(id);

                return Mapper.Map<Discipline>(discipline);
            }

            if (CurrentUser.IsLecturer())
            {
                var discipline = await _repositoryDiscipline.FindFirstAsync(new DisciplinesById(id)) ??
                    throw ExceptionHelper.NotFound<DatabaseDiscipline>(id);

                if (new DisciplinesByLecturerId(CurrentUser.Id).IsSatisfiedBy(discipline) == false)
                    throw ExceptionHelper.NoAccess();

                return Mapper.Map<Discipline>(discipline);
            }

            if (CurrentUser.IsStudent())
            {
                var discipline = await _repositoryDiscipline.FindFirstAsync(new DisciplinesById(id)) ??
                    throw ExceptionHelper.NotFound<DatabaseDiscipline>(id);

                if (new DisciplinesByStudentId(CurrentUser.Id).IsSatisfiedBy(discipline) == false)
                    throw ExceptionHelper.NoAccess();

                return Mapper.Map<Discipline>(discipline);
            }

            throw ExceptionHelper.NoAccess();
        }
    }
}