using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Extensions;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Managers;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.Models.Rest;
using EducationSystem.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Managers
{
    public sealed class ManagerDiscipline : Manager<ManagerDiscipline>, IManagerDiscipline
    {
        private readonly IHelperUserRole _helperUserRole;
        private readonly IExceptionFactory _exceptionFactory;
        private readonly IRepositoryDiscipline _repositoryDiscipline;

        public ManagerDiscipline(
            IMapper mapper,
            ILogger<ManagerDiscipline> logger,
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

            var items = disciplines
                .Select(x => Map(x, options))
                .ToList();

            return new PagedData<Discipline>(items, count);
        }

        public async Task<PagedData<Discipline>> GetDisciplinesByStudentIdAsync(int studentId, OptionsDiscipline options, FilterDiscipline filter)
        {
            await _helperUserRole.CheckRoleStudentAsync(studentId);

            var (count, disciplines) = await _repositoryDiscipline.GetDisciplinesByStudentIdAsync(studentId, filter);

            var items = disciplines
                .Select(x => MapForStudent(x, options))
                .ToList();

            return new PagedData<Discipline>(items, count);
        }

        public async Task<Discipline> GetDisciplineAsync(int id, OptionsDiscipline options)
        {
            var discipline = await _repositoryDiscipline.GetByIdAsync(id) ??
                throw _exceptionFactory.NotFound<DatabaseDiscipline>(id);

            return Map(discipline, options);
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