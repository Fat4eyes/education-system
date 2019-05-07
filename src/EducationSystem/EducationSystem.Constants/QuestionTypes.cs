using EducationSystem.Enums;

namespace EducationSystem.Constants
{
    public static class QuestionTypes
    {
        public static readonly QuestionType[] Supported =
        {
            QuestionType.ClosedOneAnswer,
            QuestionType.ClosedManyAnswers,
            QuestionType.OpenedOneString,
            QuestionType.WithProgram
        };
    }
}