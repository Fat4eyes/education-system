using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Interfaces.Services;
using EducationSystem.Models.Rest;
using EducationSystem.Specifications.Questions;
using EducationSystem.Specifications.Tests;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Services
{
    public sealed class ServiceTestProcess : Service<ServiceTestProcess>, IServiceTestProcess
    {
        private readonly IRepository<DatabaseTest> _repositoryTest;
        private readonly IRepository<DatabaseQuestion> _repositoryQuestion;
        private readonly IQuestionValidatorFactory _questionValidatorFactory;

        public ServiceTestProcess(
            IMapper mapper,
            ILogger<ServiceTestProcess> logger,
            IExecutionContext executionContext,
            IExceptionFactory exceptionFactory,
            IRepository<DatabaseTest> repositoryTest,
            IRepository<DatabaseQuestion> repositoryQuestion,
            IQuestionValidatorFactory questionValidatorFactory)
            : base(
                mapper,
                logger,
                executionContext,
                exceptionFactory)
        {
            _repositoryTest = repositoryTest;
            _repositoryQuestion = repositoryQuestion;
            _questionValidatorFactory = questionValidatorFactory;
        }

        public async Task<Question> GetQuestionAsync(int id)
        {
            if (CurrentUser.IsNotStudent())
                throw ExceptionFactory.NoAccess();

            await ValidateTestAsync(id);

            var specification =
                new QuestionsByTestId(id) &
                new QuestionsForStudents() &
                new QuestionsByStudentId(CurrentUser.Id, false);

            var question = await _repositoryQuestion.FindFirstAsync(specification);

            return Mapper.Map<Question>(question);
        }

        public async Task<Question> ProcessQuestionAsync(int id, Question question)
        {
            if (CurrentUser.IsNotStudent())
                throw ExceptionFactory.NoAccess();

            await ValidateTestAsync(id);

            var model = await _repositoryQuestion.FindFirstAsync(new QuestionsById(question.Id)) ??
                throw ExceptionFactory.NotFound<DatabaseQuestion>(question.Id);

            var result = await _questionValidatorFactory
                .GetQuestionValidator(model.Type)
                .ValidateAsync(question);

            if (question.Save != true || result.Right != true)
                return result;

            model.QuestionStudents.Add(new DatabaseQuestionStudent
            {
                Passed = true,
                StudentId = CurrentUser.Id
            });

            await _repositoryQuestion.UpdateAsync(model, true);

            return result;
        }

        private async Task ValidateTestAsync(int id)
        {
            var test = await _repositoryTest.FindFirstAsync(new TestsById(id)) ??
                throw ExceptionFactory.NotFound<DatabaseTest>(id);

            var specification =
                new TestsById(id) &
                new TestsByStudentId(CurrentUser.Id) &
                new TestsForStudents();

            if (specification.IsSatisfiedBy(test) == false)
                throw ExceptionFactory.NoAccess();
        }
    }
}