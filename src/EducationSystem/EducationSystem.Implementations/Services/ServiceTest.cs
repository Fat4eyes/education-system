using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Implementations.Specifications;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Interfaces.Services;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Rest;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Services
{
    public sealed class ServiceTest : Service<ServiceTest>, IServiceTest
    {
        private readonly IHelperUserRole _helperUserRole;
        private readonly IValidator<Test> _validatorTest;
        private readonly IExceptionFactory _exceptionFactory;
        private readonly IExecutionContext _executionContext;

        private readonly IRepository<DatabaseTest> _repositoryTest;
        private readonly IRepository<DatabaseTestTheme> _repositoryTestTheme;

        public ServiceTest(
            IMapper mapper,
            ILogger<ServiceTest> logger,
            IHelperUserRole helperUserRole,
            IValidator<Test> validatorTest,
            IExceptionFactory exceptionFactory,
            IExecutionContext executionContext,
            IRepository<DatabaseTest> repositoryTest,
            IRepository<DatabaseTestTheme> repositoryTestTheme)
            : base(mapper, logger)
        {
            _helperUserRole = helperUserRole;
            _validatorTest = validatorTest;
            _exceptionFactory = exceptionFactory;
            _executionContext = executionContext;
            _repositoryTest = repositoryTest;
            _repositoryTestTheme = repositoryTestTheme;
        }

        public async Task<PagedData<Test>> GetTestsAsync(FilterTest filter)
        {
            var specification =
                new TestsByName(filter.Name) &
                new TestsByDisciplineId(filter.DisciplineId) &
                new TestsByType(filter.TestType);

            if (filter.OnlyActive)
                specification = specification & new TestsByActive(true);

            var (count, tests) = await _repositoryTest.FindPaginatedAsync(specification, filter);

            return new PagedData<Test>(Mapper.Map<List<Test>>(tests), count);
        }

        public async Task<PagedData<Test>> GetStudentTestsAsync(int studentId, FilterTest filter)
        {
            await _helperUserRole.CheckRoleStudentAsync(studentId);

            var specification =
                new TestsByName(filter.Name) &
                new TestsByDisciplineId(filter.DisciplineId) &
                new TestsByType(filter.TestType) &
                new TestsByStudentId(studentId) &
                new TestsForStudent();

            if (filter.OnlyActive)
                specification = specification & new TestsByActive(true);

            var (count, tests) = await _repositoryTest.FindPaginatedAsync(specification, filter);

            return new PagedData<Test>(Mapper.Map<List<Test>>(tests), count);
        }

        public async Task<PagedData<Test>> GetLecturerTestsAsync(int lecturerId, FilterTest filter)
        {
            await _helperUserRole.CheckRoleLecturerAsync(lecturerId);

            var specification =
                new TestsByName(filter.Name) &
                new TestsByDisciplineId(filter.DisciplineId) &
                new TestsByType(filter.TestType) &
                new TestsByLecturerId(lecturerId);

            if (filter.OnlyActive)
                specification = specification & new TestsByActive(true);

            var (count, tests) = await _repositoryTest.FindPaginatedAsync(specification, filter);

            return new PagedData<Test>(Mapper.Map<List<Test>>(tests), count);
        }

        public async Task<Test> GetTestAsync(int id)
        {
            var test = await _repositoryTest.FindFirstAsync(new TestsById(id)) ??
                throw _exceptionFactory.NotFound<DatabaseTest>(id);

            return Mapper.Map<Test>(test);
        }

        public async Task<Test> GetStudentTestAsync(int id, int studentId)
        {
            await _helperUserRole.CheckRoleStudentAsync(studentId);

            var specification =
                new TestsById(id) &
                new TestsByStudentId(studentId) &
                new TestsForStudent();

            var test = await _repositoryTest.FindFirstAsync(specification) ??
                throw _exceptionFactory.NotFound<DatabaseTest>(id);

            return Mapper.Map<Test>(test);
        }

        public async Task<Test> GetLecturerTestAsync(int id, int lecturerId)
        {
            await _helperUserRole.CheckRoleLecturerAsync(lecturerId);

            var specification =
                new TestsById(id) &
                new TestsByLecturerId(lecturerId);

            var test = await _repositoryTest.FindFirstAsync(specification) ??
                throw _exceptionFactory.NotFound<DatabaseTest>(id);

            return Mapper.Map<Test>(test);
        }

        public async Task DeleteTestAsync(int id)
        {
            var test = await _repositoryTest.FindFirstAsync(new TestsById(id)) ??
                throw _exceptionFactory.NotFound<DatabaseTest>(id);

            var user = await _executionContext.GetCurrentUserAsync();

            if (user.IsNotAdmin() && !new TestsByLecturerId(user.Id).IsSatisfiedBy(test))
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

            var model = await _repositoryTest.FindFirstAsync(new TestsById(id)) ??
                throw _exceptionFactory.NotFound<DatabaseTest>(id);

            var user = await _executionContext.GetCurrentUserAsync();

            if (!new TestsByLecturerId(user.Id).IsSatisfiedBy(model))
                throw _exceptionFactory.NoAccess();

            Mapper.Map(Mapper.Map<DatabaseTest>(test), model);

            await _repositoryTest.UpdateAsync(model, true);

            if (model.TestThemes.Any())
                await _repositoryTestTheme.RemoveAsync(model.TestThemes, true);

            model.TestThemes = Mapper.Map<List<DatabaseTestTheme>>(test.Themes);

            await _repositoryTestTheme.AddAsync(model.TestThemes, true);
        }
    }
}