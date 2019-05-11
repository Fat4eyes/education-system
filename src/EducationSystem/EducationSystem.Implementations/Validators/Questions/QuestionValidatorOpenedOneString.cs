using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Extensions;
using EducationSystem.Helpers;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models.Rest;

namespace EducationSystem.Implementations.Validators.Questions
{
    public sealed class QuestionValidatorOpenedOneString : QuestionValidator, IQuestionValidatorOpenedOneString
    {
        private const string Pattern = "[\\,.?'@#%^&_№~><`)(}{][|;:\"!-+-*/]";

        public QuestionValidatorOpenedOneString(
            IMapper mapper,
            IHashComputer hashComputer,
            IExecutionContext executionContext,
            IRepository<DatabaseQuestion> repositoryQuestion)
            : base(
                mapper,
                hashComputer,
                executionContext,
                repositoryQuestion)
        { }

        public override async Task<Question> ValidateAsync(Question question)
        {
            var result = await base.ValidateAsync(question);

            if (question.Answers.IsEmpty())
                throw ExceptionHelper.CreatePublicException("Не указан ответ на вопрос.");

            var answer = question.Answers.First();

            if (string.IsNullOrWhiteSpace(answer.Text))
                throw ExceptionHelper.CreatePublicException("Не указан текст ответа на вопрос.");

            var text = Regex
                .Replace(answer.Text, Pattern, string.Empty)
                .ToLowerInvariant();

            var model = await GetQuestionModelAsync(question.Id);

            var texts = model.Answers
                .Select(x => Regex
                    .Replace(x.Text, Pattern, string.Empty)
                    .ToLowerInvariant())
                .ToList();

            return result.SetRight(texts.Contains(text));
        }
    }
}