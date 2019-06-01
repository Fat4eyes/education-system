using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Helpers;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Code;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models.Rest;

namespace EducationSystem.Implementations.Validators.Questions
{
    public sealed class QuestionValidatorWithProgram : QuestionValidator, IQuestionValidatorWithProgram
    {
        private readonly ICodeRunner _codeRunner;

        public QuestionValidatorWithProgram(
            IMapper mapper,
            IContext context,
            IHashComputer hashComputer,
            IRepository<DatabaseQuestion> repositoryQuestion,
            ICodeRunner codeRunner)
            : base(
                mapper,
                context,
                hashComputer,
                repositoryQuestion)
        {
            _codeRunner = codeRunner;
        }

        public override async Task<Question> ValidateAsync(Question question)
        {
            var result = await base.ValidateAsync(question);

            if (question.Program == null)
                throw ExceptionHelper.CreatePublicException("Не указана программа.");

            if (string.IsNullOrWhiteSpace(question.Program.Source))
                throw ExceptionHelper.CreatePublicException("Не указан исходный код программы.");

            var response = await _codeRunner.RunAsync(question.Program);

            if (response?.CodeExecutionResult == null)
                return result.SetRight(false);

            return result
                .SetCodeRunningResult(response)
                .SetRight(response.CodeExecutionResult.Success);
        }
    }
}