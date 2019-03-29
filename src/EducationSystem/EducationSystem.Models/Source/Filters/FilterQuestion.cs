using System.Collections.Generic;
using EducationSystem.Enums.Source;

namespace EducationSystem.Models.Source.Filters
{
    public class FilterQuestion : Filter
    {
        public List<QuestionType> QuestionTypes { get; set; } = null;

        public int Count { get; set; } = 10;
    }
}