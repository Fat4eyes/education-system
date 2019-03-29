using System.Collections.Generic;
using EducationSystem.Enums.Source;

namespace EducationSystem.Models.Source.Filters
{
    public class FilterQuestion : Filter
    {
        public List<QuestionType> QuestionTypes { get; set; } = new List<QuestionType>
        {
            QuestionType.ClosedOneAnswer,
            QuestionType.ClosedManyAnswers,
            QuestionType.WithProgram
        };

        public int Count { get; set; } = 10;
    }
}