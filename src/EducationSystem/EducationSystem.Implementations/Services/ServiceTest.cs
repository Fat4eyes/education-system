using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Services;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models;
using EducationSystem.Models.Datas;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.Models.Rest;
using EducationSystem.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Services
{
    public sealed class ServiceTest : Service<ServiceTest>, IServiceTest
    {
        private readonly IHelperUserRole _helperUserRole;
        private readonly IValidator<Test> _validatorTest;
        private readonly IExceptionFactory _exceptionFactory;
        private readonly IExecutionContext _executionContext;

        private readonly IRepositoryTest _repositoryTest;
        private readonly IRepositoryTestTheme _repositoryTestTheme;

        public ServiceTest(
            IMapper mapper,
            ILogger<ServiceTest> logger,
            IHelperUserRole helperUserRole,
            IValidator<Test> validatorTest,
            IExceptionFactory exceptionFactory,
            IExecutionContext executionContext,
            IRepositoryTest repositoryTest,
            IRepositoryTestTheme repositoryTestTheme)
            : base(mapper, logger)
        {
            _helperUserRole = helperUserRole;
            _validatorTest = validatorTest;
            _exceptionFactory = exceptionFactory;
            _executionContext = executionContext;
            _repositoryTest = repositoryTest;
            _repositoryTestTheme = repositoryTestTheme;
        }

        public async Task<PagedData<Test>> GetTestsAsync(OptionsTest options, FilterTest filter)
        {
            var (count, tests) = await _repositoryTest.GetTestsAsync(filter);

            return new PagedData<Test>(tests.Select(x => Map(x, options)).ToList(), count);
        }

        public async Task<PagedData<Test>> GetStudentTestsAsync(int studentId, OptionsTest options, FilterTest filter)
        {
            await _helperUserRole.CheckRoleStudentAsync(studentId);

            var (count, tests) = await _repositoryTest.GetStudentTestsAsync(studentId, filter);

            return new PagedData<Test>(tests.Select(x => MapForStudent(x, options, studentId)).ToList(), count);
        }

        public async Task<PagedData<Test>> GetLecturerTestsAsync(int lecturerId, OptionsTest options, FilterTest filter)
        {
            await _helperUserRole.CheckRoleLecturerAsync(lecturerId);

            var (count, tests) = await _repositoryTest.GetLecturerTestsAsync(lecturerId, filter);

            return new PagedData<Test>(tests.Select(x => Map(x, options)).ToList(), count);
        }

        public async Task<Test> GetTestAsync(int id, OptionsTest options)
        {
            var test = await _repositoryTest.GetByIdAsync(id) ??
                throw _exceptionFactory.NotFound<DatabaseTest>(id);

            return Map(test, options);
        }

        public async Task<Test> GetStudentTestAsync(int id, int studentId, OptionsTest options)
        {
            await _helperUserRole.CheckRoleStudentAsync(studentId);

            var test = await _repositoryTest.GetStudentTestAsync(id, studentId) ??
                throw _exceptionFactory.NotFound<DatabaseTest>(id);

            return MapForStudent(test, options, studentId);
        }

        public async Task<Test> GetLecturerTestAsync(int id, int lecturerId, OptionsTest options)
        {
            await _helperUserRole.CheckRoleLecturerAsync(lecturerId);

            var test = await _repositoryTest.GetLecturerTestAsync(id, lecturerId) ??
                throw _exceptionFactory.NotFound<DatabaseTest>(id);

            return Map(test, options);
        }

        public async Task DeleteTestAsync(int id)
        {
            var test = await _repositoryTest.GetByIdAsync(id) ??
                throw _exceptionFactory.NotFound<DatabaseTest>(id);

            var user = await _executionContext.GetCurrentUserAsync();

            if (user.IsNotAdmin() && test.Discipline?.Lecturers?.All(x => x.LecturerId != user.Id) != false)
                throw _exceptionFactory.NoAccess();

            await _repositoryTest.RemoveAsync(test, true);
        }

        public async Task<int> CreateTestAsync(Test test)
        {
            await _validatorTest.ValidateAsync(test.Format());

            var model = Mapper.Map<DatabaseTest>(test);

            await _repositoryTest.AddAsync(model, true);

            return model.Id;
        }

        public async Task UpdateTestAsync(int id, Test test)
        {
            await _validatorTest.ValidateAsync(test.Format());

            var model = await _repositoryTest.GetByIdAsync(id) ??
                throw _exceptionFactory.NotFound<DatabaseTest>(id);

            var user = await _executionContext.GetCurrentUserAsync();

            if (model.Discipline?.Lecturers?.All(x => x.LecturerId != user.Id) != false)
                throw _exceptionFactory.NoAccess();

            Mapper.Map(Mapper.Map<DatabaseTest>(test), model);

            await _repositoryTest.UpdateAsync(model, true);

            if (model.TestThemes.Any())
                await _repositoryTestTheme.RemoveAsync(model.TestThemes, true);

            model.TestThemes = Mapper.Map<List<DatabaseTestTheme>>(test.Themes);

            await _repositoryTestTheme.AddAsync(model.TestThemes, true);
        }

        private Test Map(DatabaseTest test, OptionsTest options)
        {
            return Mapper.Map<DatabaseTest, Test>(test, x =>
            {
                x.AfterMap((s, d) =>
                {
                    if (options.WithThemes)
                        d.Themes = Mapper.Map<List<Theme>>(s.TestThemes.Select(y => y.Theme));
                });
            });
        }

        private Test MapForStudent(DatabaseTest test, OptionsTest options, int studentId)
        {
            return Mapper.Map<DatabaseTest, Test>(test, x =>
            {
                x.AfterMap((s, d) =>
                {
                    if (!options.WithData)
                        return;

                    var themes = s.TestThemes
                        .Select(y => y.Theme)
                        .ToArray();

                    d.Data = TestData.Create(themes, studentId);
                });
            });
        }
    }
}