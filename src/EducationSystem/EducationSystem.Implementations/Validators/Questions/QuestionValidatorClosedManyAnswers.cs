using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Interfaces.Validators;

namespace EducationSystem.Implementations.Validators.Questions
{
    public sealed class QuestionValidatorClosedManyAnswers : QuestionValidatorClosedAnswer, IQuestionValidatorClosedManyAnswers
    {
        public QuestionValidatorClosedManyAnswers(
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
    }
}