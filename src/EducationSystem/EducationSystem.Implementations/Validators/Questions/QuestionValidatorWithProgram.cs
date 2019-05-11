using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Helpers;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models.Rest;

namespace EducationSystem.Implementations.Validators.Questions
{
    public sealed class QuestionValidatorWithProgram : QuestionValidator, IQuestionValidatorWithProgram
    {
        private readonly ICodeExecutor _codeExecutor;

        public QuestionValidatorWithProgram(
            IMapper mapper,
            IHashComputer hashComputer,
            IExecutionContext executionContext,
            IRepository<DatabaseQuestion> repositoryQuestion,
            ICodeExecutor codeExecutor)
            : base(
                mapper,
                hashComputer,
                executionContext,
                repositoryQuestion)
        {
            _codeExecutor = codeExecutor;
        }

        public override async Task<Question> ValidateAsync(Question question)
        {
            var result = await base.ValidateAsync(question);

            if (question.Program == null)
                throw ExceptionHelper.CreatePublicException("Не указана программа.");

            if (string.IsNullOrWhiteSpace(question.Program.Source))
                throw ExceptionHelper.CreatePublicException("Не указан исходный код программы.");

            var response = await _codeExecutor.ExecuteAsync(question.Program);

            return result.SetRight(response.Success);
        }
    }
}