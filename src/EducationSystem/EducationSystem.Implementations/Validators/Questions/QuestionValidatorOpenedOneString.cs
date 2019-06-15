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
        private const string Space = " ";

        private const string Pattern = "[^0-9a-zA-Zа-яА-Я]+";

        public QuestionValidatorOpenedOneString(
            IMapper mapper,
            IContext context,
            IHashComputer hashComputer,
            IRepository<DatabaseQuestion> repositoryQuestion)
            : base(
                mapper,
                context,
                hashComputer,
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

            var model = await GetQuestionModelAsync(question.Id);

            var texts = model.Answers
                .Select(x => ProcessText(x.Text))
                .ToList();

            return result.SetRight(texts.Contains(ProcessText(answer.Text)));
        }

        private static string ProcessText(string text)
        {
            // Убираем все символы, кроме букв и цифр.
            var result = Regex
                .Replace(text, Pattern, Space)
                .ToLowerInvariant();

            // Убираем дублирующиеся пробелы.
            return Regex.Replace(result, @"\s+", " ");
        }
    }
}