using EducationSystem.Enums;
using EducationSystem.Interfaces.Validators;

namespace EducationSystem.Interfaces.Factories
{
    public interface IQuestionValidatorFactory
    {
        IQuestionValidator GetQuestionValidator(QuestionType type);
    }
}
