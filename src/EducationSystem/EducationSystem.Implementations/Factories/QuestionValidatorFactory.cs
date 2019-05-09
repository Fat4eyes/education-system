using System;
using EducationSystem.Enums;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Interfaces.Validators;

namespace EducationSystem.Implementations.Factories
{
    public sealed class QuestionValidatorFactory : IQuestionValidatorFactory
    {
        private readonly IQuestionValidatorClosedOneAnswer _questionValidatorClosedOneAnswer;
        private readonly IQuestionValidatorClosedManyAnswers _questionValidatorClosedManyAnswers;
        private readonly IQuestionValidatorOpenedOneString _questionValidatorOpenedOneString;
        private readonly IQuestionValidatorWithProgram _questionValidatorWithProgram;

        public QuestionValidatorFactory(
            IQuestionValidatorClosedOneAnswer questionValidatorClosedOneAnswer,
            IQuestionValidatorClosedManyAnswers questionValidatorClosedManyAnswers,
            IQuestionValidatorOpenedOneString questionValidatorOpenedOneString,
            IQuestionValidatorWithProgram questionValidatorWithProgram)
        {
            _questionValidatorClosedOneAnswer = questionValidatorClosedOneAnswer;
            _questionValidatorClosedManyAnswers = questionValidatorClosedManyAnswers;
            _questionValidatorOpenedOneString = questionValidatorOpenedOneString;
            _questionValidatorWithProgram = questionValidatorWithProgram;
        }

        public IQuestionValidator GetQuestionValidator(QuestionType type)
        {
            switch (type)
            {
                case QuestionType.ClosedOneAnswer:
                    return _questionValidatorClosedOneAnswer;
                case QuestionType.ClosedManyAnswers:
                    return _questionValidatorClosedManyAnswers;
                case QuestionType.OpenedOneString:
                    return _questionValidatorOpenedOneString;
                case QuestionType.WithProgram:
                    return _questionValidatorWithProgram;
            }

            throw new ArgumentOutOfRangeException(nameof(type));
        }
    }
}