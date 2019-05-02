using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Extensions;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Services;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
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

        public async Task<PagedData<Discipline>> GetDisciplinesAsync(OptionsDiscipline options, FilterDiscipline filter)
        {
            var (count, disciplines) = await _repositoryDiscipline.GetDisciplinesAsync(filter);

            return new PagedData<Discipline>(disciplines.Select(x => Map(x, options)).ToList(), count);
        }

        public async Task<PagedData<Discipline>> GetStudentDisciplinesAsync(int studentId, OptionsDiscipline options, FilterDiscipline filter)
        {
            await _helperUserRole.CheckRoleStudentAsync(studentId);

            var (count, disciplines) = await _repositoryDiscipline.GetStudentDisciplinesAsync(studentId, filter);

            return new PagedData<Discipline>(disciplines.Select(x => MapForStudent(x, options)).ToList(), count);
        }

        public async Task<PagedData<Discipline>> GetLecturerDisciplinesAsync(int lecturerId, OptionsDiscipline options, FilterDiscipline filter)
        {
            await _helperUserRole.CheckRoleLecturerAsync(lecturerId);

            var (count, disciplines) = await _repositoryDiscipline.GetLecturerDisciplinesAsync(lecturerId, filter);

            return new PagedData<Discipline>(disciplines.Select(x => MapForLecturer(x, options)).ToList(), count);
        }

        public async Task<Discipline> GetDisciplineAsync(int id, OptionsDiscipline options)
        {
            var discipline = await _repositoryDiscipline.GetByIdAsync(id) ??
                throw _exceptionFactory.NotFound<DatabaseDiscipline>(id);

            return Map(discipline, options);
        }

        public async Task<Discipline> GetStudentDisciplineAsync(int id, int studentId, OptionsDiscipline options)
        {
            await _helperUserRole.CheckRoleStudentAsync(studentId);

            var discipline = await _repositoryDiscipline.GetStudentDisciplineAsync(id, studentId) ??
                throw _exceptionFactory.NotFound<DatabaseDiscipline>(id);

            return MapForStudent(discipline, options);
        }

        public async Task<Discipline> GetLecturerDisciplineAsync(int id, int lecturerId, OptionsDiscipline options)
        {
            await _helperUserRole.CheckRoleLecturerAsync(lecturerId);

            var discipline = await _repositoryDiscipline.GetLecturerDisciplineAsync(id, lecturerId) ??
                throw _exceptionFactory.NotFound<DatabaseDiscipline>(id);

            return MapForLecturer(discipline, options);
        }

        private Discipline Map(DatabaseDiscipline discipline, OptionsDiscipline options)
        {
            return Mapper.Map<DatabaseDiscipline, Discipline>(discipline, x =>
            {
                x.AfterMap((s, d) =>
                {
                    if (options.WithTests)
                        d.Tests = Mapper.Map<List<Test>>(s.Tests);

                    if (options.WithThemes)
                        d.Themes = Mapper.Map<List<Theme>>(s.Themes);

                    if (options.WithLecturers)
                        d.Lecturers = Mapper.Map<List<User>>(s.Lecturers.Select(y => y.Lecturer));
                });
            });
        }

        private Discipline MapForLecturer(DatabaseDiscipline discipline, OptionsDiscipline options)
        {
            return Mapper.Map<DatabaseDiscipline, Discipline>(discipline, x =>
            {
                x.AfterMap((s, d) =>
                {
                    if (options.WithTests)
                        d.Tests = Mapper.Map<List<Test>>(s.Tests);

                    if (options.WithThemes)
                        d.Themes = Mapper.Map<List<Theme>>(s.Themes);
                });
            });
        }

        private Discipline MapForStudent(DatabaseDiscipline discipline, OptionsDiscipline options)
        {
            return Mapper.Map<DatabaseDiscipline, Discipline>(discipline, x =>
            {
                x.AfterMap((s, d) =>
                {
                    if (options.WithTests)
                    {
                        d.Tests = s.Tests
                            .Where(y => y.IsActive == 1)
                            .Where(y => y.TestThemes.IsNotEmpty(z => z.Theme?.Questions.IsNotEmpty() == true))
                            .Select(y => Mapper.Map<Test>(y))
                            .ToList();
                    }

                    if (options.WithThemes)
                    {
                        d.Themes = s.Themes
                            .Where(y => y.Questions.IsNotEmpty())
                            .Select(y => Mapper.Map<Theme>(y))
                            .ToList();
                    }
                });
            });
        }
    }
}