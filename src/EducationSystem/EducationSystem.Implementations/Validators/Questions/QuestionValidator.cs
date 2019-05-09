using System;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models.Rest;
using EducationSystem.Specifications.Questions;

namespace EducationSystem.Implementations.Validators.Questions
{
    public abstract class QuestionValidator : IQuestionValidator
    {
        protected readonly IMapper Mapper;
        protected readonly IHashComputer HashComputer;
        protected readonly IExecutionContext ExecutionContext;
        protected readonly IExceptionFactory ExceptionFactory;
        protected readonly IRepository<DatabaseQuestion> RepositoryQuestion;

        protected static readonly Random Random = new Random();

        protected QuestionValidator(
            IMapper mapper,
            IHashComputer hashComputer,
            IExecutionContext executionContext,
            IExceptionFactory exceptionFactory,
            IRepository<DatabaseQuestion> repositoryQuestion)
        {
            Mapper = mapper;
            ExecutionContext = executionContext;
            ExceptionFactory = exceptionFactory;
            HashComputer = hashComputer;
            RepositoryQuestion = repositoryQuestion;
        }

        public virtual async Task<Question> ValidateAsync(Question question)
        {
            if (question == null)
                throw new ArgumentNullException(nameof(question));

            var user = await ExecutionContext.GetCurrentUserAsync();
            var model = await GetQuestionModelAsync(question.Id);

            var specification =
                new QuestionsForStudents() &
                new QuestionsByTestId(question.TestId) &
                new QuestionsByStudentId(user.Id, false);

            if (specification.IsSatisfiedBy(model) == false)
                throw ExceptionFactory.NoAccess();

            var hash = await HashComputer.ComputeForQuestionAsync(model);

            if (string.Equals(hash, question.Hash, StringComparison.InvariantCulture) == false)
                throw ExceptionFactory.NoAccess();

            return Mapper
                .Map<Question>(model)
                .SetRight(true);
        }

        protected async Task<DatabaseQuestion> GetQuestionModelAsync(int id)
        {
            return await RepositoryQuestion.FindFirstAsync(new QuestionsById(id)) ??
                throw ExceptionFactory.NotFound<DatabaseQuestion>(id);
        }
    }
}