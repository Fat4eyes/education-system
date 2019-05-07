using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Interfaces.Services;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Rest;
using EducationSystem.Specifications.Tests;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Services
{
    public sealed class ServiceTest : Service<ServiceTest>, IServiceTest
    {
        private readonly IValidator<Test> _validatorTest;
        private readonly IRepository<DatabaseTest> _repositoryTest;
        private readonly IRepository<DatabaseTestTheme> _repositoryTestTheme;

        public ServiceTest(
            IMapper mapper,
            ILogger<ServiceTest> logger,
            IValidator<Test> validatorTest,
            IExceptionFactory exceptionFactory,
            IExecutionContext executionContext,
            IRepository<DatabaseTest> repositoryTest,
            IRepository<DatabaseTestTheme> repositoryTestTheme)
            : base(
                mapper,
                logger,
                executionContext,
                exceptionFactory)
        {
            _validatorTest = validatorTest;
            _repositoryTest = repositoryTest;
            _repositoryTestTheme = repositoryTestTheme;
        }

        public async Task<PagedData<Test>> GetTestsAsync(FilterTest filter)
        {
            if (CurrentUser.IsAdmin())
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

            if (CurrentUser.IsLecturer())
            {
                var specification =
                    new TestsByName(filter.Name) &
                    new TestsByDisciplineId(filter.DisciplineId) &
                    new TestsByType(filter.TestType) &
                    new TestsByLecturerId(CurrentUser.Id);

                if (filter.OnlyActive)
                    specification = specification & new TestsByActive(true);

                var (count, tests) = await _repositoryTest.FindPaginatedAsync(specification, filter);

                return new PagedData<Test>(Mapper.Map<List<Test>>(tests), count);
            }

            if (CurrentUser.IsStudent())
            {
                var specification =
                    new TestsByName(filter.Name) &
                    new TestsByDisciplineId(filter.DisciplineId) &
                    new TestsByType(filter.TestType) &
                    new TestsByStudentId(CurrentUser.Id) &
                    new TestsForStudents();

                var (count, tests) = await _repositoryTest.FindPaginatedAsync(specification, filter);

                return new PagedData<Test>(Mapper.Map<List<Test>>(tests), count);
            }

            throw ExceptionFactory.NoAccess();
        }

        public async Task<Test> GetTestAsync(int id)
        {
            if (CurrentUser.IsAdmin())
            {
                var test = await _repositoryTest.FindFirstAsync(new TestsById(id)) ??
                    throw ExceptionFactory.NotFound<DatabaseTest>(id);

                return Mapper.Map<Test>(test);
            }

            if (CurrentUser.IsLecturer())
            {
                var test = await _repositoryTest.FindFirstAsync(new TestsById(id)) ??
                    throw ExceptionFactory.NotFound<DatabaseTest>(id);

                if (new TestsByLecturerId(CurrentUser.Id).IsSatisfiedBy(test) == false)
                    throw ExceptionFactory.NoAccess();

                return Mapper.Map<Test>(test);
            }

            if (CurrentUser.IsStudent())
            {
                var test = await _repositoryTest.FindFirstAsync(new TestsById(id)) ??
                    throw ExceptionFactory.NotFound<DatabaseTest>(id);

                var specification =
                    new TestsByStudentId(CurrentUser.Id) &
                    new TestsForStudents();

                if (specification.IsSatisfiedBy(test) == false)
                    throw ExceptionFactory.NoAccess();

                return Mapper.Map<Test>(test);
            }

            throw ExceptionFactory.NoAccess();
        }

        public async Task DeleteTestAsync(int id)
        {
            if (CurrentUser.IsNotAdmin() && CurrentUser.IsNotLecturer())
                throw ExceptionFactory.NoAccess();

            var test = await _repositoryTest.FindFirstAsync(new TestsById(id)) ??
                throw ExceptionFactory.NotFound<DatabaseTest>(id);

            if (CurrentUser.IsNotAdmin() && !new TestsByLecturerId(CurrentUser.Id).IsSatisfiedBy(test))
                throw ExceptionFactory.NoAccess();

            await _repositoryTest.RemoveAsync(test, true);
        }

        public async Task<int> CreateTestAsync(Test test)
        {
            if (CurrentUser.IsNotLecturer())
                throw ExceptionFactory.NoAccess();

            await _validatorTest.ValidateAsync(test.Format());

            var model = Mapper.Map<DatabaseTest>(test);

            await _repositoryTest.AddAsync(model, true);

            return model.Id;
        }

        public async Task UpdateTestAsync(int id, Test test)
        {
            if (CurrentUser.IsNotLecturer())
                throw ExceptionFactory.NoAccess();

            await _validatorTest.ValidateAsync(test.Format());

            var model = await _repositoryTest.FindFirstAsync(new TestsById(id)) ??
                throw ExceptionFactory.NotFound<DatabaseTest>(id);

            if (!new TestsByLecturerId(CurrentUser.Id).IsSatisfiedBy(model))
                throw ExceptionFactory.NoAccess();

            Mapper.Map(Mapper.Map<DatabaseTest>(test), model);

            await _repositoryTest.UpdateAsync(model, true);

            if (model.TestThemes.Any())
                await _repositoryTestTheme.RemoveAsync(model.TestThemes, true);

            model.TestThemes = Mapper.Map<List<DatabaseTestTheme>>(test.Themes);

            await _repositoryTestTheme.AddAsync(model.TestThemes, true);
        }
    }
}