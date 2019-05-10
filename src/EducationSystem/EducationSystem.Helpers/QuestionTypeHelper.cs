using EducationSystem.Enums;

namespace EducationSystem.Helpers
{
    public static class QuestionTypeHelper
    {
        public static QuestionType[] GetSupportedTypes()
        {
            return new[]
            {
                QuestionType.ClosedOneAnswer,
                QuestionType.ClosedManyAnswers,
                QuestionType.OpenedOneString,
                QuestionType.WithProgram
            };
        }
    }
}