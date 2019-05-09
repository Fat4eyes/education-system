using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Enums;
using EducationSystem.Exceptions.Helpers;
using EducationSystem.Extensions;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Models.Rest;

namespace EducationSystem.Implementations.Validators.Questions
{
    public abstract class QuestionValidatorClosedAnswer : QuestionValidator
    {
        protected QuestionValidatorClosedAnswer(
            IMapper mapper,
            IHashComputer hashComputer,
            IExecutionContext executionContext,
            IExceptionFactory exceptionFactory,
            IRepository<DatabaseQuestion> repositoryQuestion)
            : base(
                mapper,
                hashComputer,
                executionContext,
                exceptionFactory,
                repositoryQuestion)
        { }

        public override async Task<Question> ValidateAsync(Question question)
        {
            var result = await base.ValidateAsync(question);

            if (question.Answers.IsEmpty())
                throw ExceptionHelper.CreatePublicException("Не указан ни один ответ на вопрос.");

            var model = await GetQuestionModelAsync(question.Id);

            var ids = model.Answers
                .Select(y => y.Id)
                .ToArray();

            if (question.Answers.All(x => ids.Contains(x.Id)) == false)
                throw ExceptionHelper.CreatePublicException("Один из указанных вариантов ответа не принадлежит вопросу.");

            var rights = model.Answers
                .Where(x => x.IsRight)
                .Select(x => x.Id)
                .ToList();

            result.Answers.ForEach(x =>
            {
                // Был указан ответ
                // Он правильный.
                if (question.Answers.Any(y => y.Id == x.Id && rights.Contains(x.Id)))
                    x.Status = AnswerStatus.Right;

                // Был указан ответ.
                // Он неправильный.
                if (question.Answers.Any(y => y.Id == x.Id))
                    x.Status = AnswerStatus.Wrong;

                // Ответ не был указан.
                // Он правильный.
                if (rights.Contains(x.Id))
                    x.Status = AnswerStatus.Ignore;
            });

            return result.SetRight(result.Answers.All(x => x.Status == null || x.Status == AnswerStatus.Right));
        }
    }
}