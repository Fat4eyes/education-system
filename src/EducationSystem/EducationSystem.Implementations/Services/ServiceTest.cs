using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Database.Models;
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

        public async Task<PagedData<Test>> GetTestsAsync(FilterTest filter)
        {
            var (count, tests) = await _repositoryTest.GetTestsAsync(filter);

            return new PagedData<Test>(Mapper.Map<List<Test>>(tests), count);
        }

        public async Task<PagedData<Test>> GetStudentTestsAsync(int studentId, FilterTest filter)
        {
            await _helperUserRole.CheckRoleStudentAsync(studentId);

            var (count, tests) = await _repositoryTest.GetStudentTestsAsync(studentId, filter);

            return new PagedData<Test>(Mapper.Map<List<Test>>(tests), count);
        }

        public async Task<PagedData<Test>> GetLecturerTestsAsync(int lecturerId, FilterTest filter)
        {
            await _helperUserRole.CheckRoleLecturerAsync(lecturerId);

            var (count, tests) = await _repositoryTest.GetLecturerTestsAsync(lecturerId, filter);

            return new PagedData<Test>(Mapper.Map<List<Test>>(tests), count);
        }

        public async Task<Test> GetTestAsync(int id)
        {
            var test = await _repositoryTest.GetByIdAsync(id) ??
                throw _exceptionFactory.NotFound<DatabaseTest>(id);

            return Mapper.Map<Test>(test);
        }

        public async Task<Test> GetStudentTestAsync(int id, int studentId)
        {
            await _helperUserRole.CheckRoleStudentAsync(studentId);

            var test = await _repositoryTest.GetStudentTestAsync(id, studentId) ??
                throw _exceptionFactory.NotFound<DatabaseTest>(id);

            return Mapper.Map<Test>(test);
        }

        public async Task<Test> GetLecturerTestAsync(int id, int lecturerId)
        {
            await _helperUserRole.CheckRoleLecturerAsync(lecturerId);

            var test = await _repositoryTest.GetLecturerTestAsync(id, lecturerId) ??
                throw _exceptionFactory.NotFound<DatabaseTest>(id);

            return Mapper.Map<Test>(test);
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
    }
}