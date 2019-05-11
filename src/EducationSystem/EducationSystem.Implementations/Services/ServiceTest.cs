using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Extensions;
using EducationSystem.Helpers;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Interfaces.Services;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Rest;
using EducationSystem.Specifications.Questions;
using EducationSystem.Specifications.Tests;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Services
{
    public sealed class ServiceTest : Service<ServiceTest>, IServiceTest
    {
        private readonly IValidator<Test> _validatorTest;
        private readonly IRepository<DatabaseTest> _repositoryTest;
        private readonly IRepository<DatabaseQuestion> _repositoryQuestion;
        private readonly IRepository<DatabaseTestTheme> _repositoryTestTheme;
        private readonly IRepository<DatabaseQuestionStudent> _repositoryQuestionStudent;

        public ServiceTest(
            IMapper mapper,
            ILogger<ServiceTest> logger,
            IValidator<Test> validatorTest,
            IExecutionContext executionContext,
            IRepository<DatabaseTest> repositoryTest,
            IRepository<DatabaseQuestion> repositoryQuestion,
            IRepository<DatabaseTestTheme> repositoryTestTheme,
            IRepository<DatabaseQuestionStudent> repositoryQuestionStudent)
            : base(
                mapper,
                logger,
                executionContext)
        {
            _validatorTest = validatorTest;
            _repositoryTest = repositoryTest;
            _repositoryQuestion = repositoryQuestion;
            _repositoryTestTheme = repositoryTestTheme;
            _repositoryQuestionStudent = repositoryQuestionStudent;
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

            throw ExceptionHelper.NoAccess();
        }

        public async Task<Test> GetTestAsync(int id)
        {
            if (CurrentUser.IsAdmin())
            {
                var test = await _repositoryTest.FindFirstAsync(new TestsById(id)) ??
                    throw ExceptionHelper.NotFound<DatabaseTest>(id);

                return Mapper.Map<Test>(test);
            }

            if (CurrentUser.IsLecturer())
            {
                var test = await _repositoryTest.FindFirstAsync(new TestsById(id)) ??
                    throw ExceptionHelper.NotFound<DatabaseTest>(id);

                if (new TestsByLecturerId(CurrentUser.Id).IsSatisfiedBy(test) == false)
                    throw ExceptionHelper.NoAccess();

                return Mapper.Map<Test>(test);
            }

            if (CurrentUser.IsStudent())
            {
                var test = await _repositoryTest.FindFirstAsync(new TestsById(id)) ??
                    throw ExceptionHelper.NotFound<DatabaseTest>(id);

                var specification =
                    new TestsByStudentId(CurrentUser.Id) &
                    new TestsForStudents();

                if (specification.IsSatisfiedBy(test) == false)
                    throw ExceptionHelper.NoAccess();

                return Mapper.Map<Test>(test);
            }

            throw ExceptionHelper.NoAccess();
        }

        public async Task DeleteTestAsync(int id)
        {
            if (CurrentUser.IsNotAdmin() && CurrentUser.IsNotLecturer())
                throw ExceptionHelper.NoAccess();

            var test = await _repositoryTest.FindFirstAsync(new TestsById(id)) ??
                throw ExceptionHelper.NotFound<DatabaseTest>(id);

            if (CurrentUser.IsNotAdmin() && !new TestsByLecturerId(CurrentUser.Id).IsSatisfiedBy(test))
                throw ExceptionHelper.NoAccess();

            await _repositoryTest.RemoveAsync(test, true);
        }

        public async Task<int> CreateTestAsync(Test test)
        {
            if (CurrentUser.IsNotLecturer())
                throw ExceptionHelper.NoAccess();

            await _validatorTest.ValidateAsync(test.Format());

            var model = Mapper.Map<DatabaseTest>(test);

            await _repositoryTest.AddAsync(model, true);

            return model.Id;
        }

        public async Task UpdateTestAsync(int id, Test test)
        {
            if (CurrentUser.IsNotLecturer())
                throw ExceptionHelper.NoAccess();

            await _validatorTest.ValidateAsync(test.Format());

            var model = await _repositoryTest.FindFirstAsync(new TestsById(id)) ??
                throw ExceptionHelper.NotFound<DatabaseTest>(id);

            if (!new TestsByLecturerId(CurrentUser.Id).IsSatisfiedBy(model))
                throw ExceptionHelper.NoAccess();

            Mapper.Map(Mapper.Map<DatabaseTest>(test), model);

            await _repositoryTest.UpdateAsync(model, true);

            if (model.TestThemes.Any())
                await _repositoryTestTheme.RemoveAsync(model.TestThemes, true);

            model.TestThemes = Mapper.Map<List<DatabaseTestTheme>>(test.Themes);

            await _repositoryTestTheme.AddAsync(model.TestThemes, true);
        }

        public async Task DeleteTestResultsAsync(int id)
        {
            if (CurrentUser.IsStudent() == false)
                throw ExceptionHelper.NoAccess();

            await ValidateTestAsync(id);

            var specification =
                new QuestionsByTestId(id) &
                new QuestionsForStudents() &
                new QuestionsByStudentId(CurrentUser.Id);

            var questions = await _repositoryQuestion.FindAllAsync(specification);

            questions.ForEach(question =>
            {
                var models = question.QuestionStudents
                    .Where(x => x.StudentId == CurrentUser.Id)
                    .ToList();

                if (models.IsEmpty())
                    return;

                question.QuestionStudents = question.QuestionStudents
                    .Except(models)
                    .ToList();

                _repositoryQuestionStudent.RemoveAsync(models);
            });

            await _repositoryQuestion.UpdateAsync(questions, true);
        }

        private async Task ValidateTestAsync(int id)
        {
            var test = await _repositoryTest.FindFirstAsync(new TestsById(id)) ??
                throw ExceptionHelper.NotFound<DatabaseTest>(id);

            var specification =
                new TestsById(id) &
                new TestsForStudents() &
                new TestsByStudentId(CurrentUser.Id);

            if (specification.IsSatisfiedBy(test) == false)
                throw ExceptionHelper.NoAccess();
        }
    }
}