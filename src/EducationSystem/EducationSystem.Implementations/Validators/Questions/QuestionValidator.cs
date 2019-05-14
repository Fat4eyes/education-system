using System;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Helpers;
using EducationSystem.Interfaces;
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
        protected readonly IContext Context;
        protected readonly IRepository<DatabaseQuestion> RepositoryQuestion;

        protected QuestionValidator(
            IMapper mapper,
            IContext context,
            IHashComputer hashComputer,
            IRepository<DatabaseQuestion> repositoryQuestion)
        {
            Mapper = mapper;
            Context = context;
            HashComputer = hashComputer;
            RepositoryQuestion = repositoryQuestion;
        }

        public virtual async Task<Question> ValidateAsync(Question question)
        {
            if (question == null)
                throw new ArgumentNullException(nameof(question));

            var user = await Context.GetCurrentUserAsync();
            var model = await GetQuestionModelAsync(question.Id);

            var specification =
                new QuestionsForStudents() &
                new QuestionsByTestId(question.TestId) &
                new QuestionsByStudentId(user.Id, false);

            if (specification.IsSatisfiedBy(model) == false)
                throw ExceptionHelper.CreatePublicException("Указанный вопрос недоступен или уже пройден.");

            var hash = await HashComputer.ComputeForQuestionAsync(model);

            if (string.Equals(hash, question.Hash, StringComparison.InvariantCulture) == false)
                throw ExceptionHelper.CreatePublicException("Указанный вопрос недоступен.");

            return Mapper
                .Map<Question>(model)
                .SetRight(true);
        }

        protected async Task<DatabaseQuestion> GetQuestionModelAsync(int id)
        {
            return await RepositoryQuestion.FindFirstAsync(new QuestionsById(id)) ??
                throw ExceptionHelper.NotFound<DatabaseQuestion>(id);
        }
    }
}