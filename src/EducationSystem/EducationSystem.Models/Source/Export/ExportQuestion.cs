using System.Collections.Generic;
using EducationSystem.Enums.Source;

namespace EducationSystem.Models.Source.Export
{
    public class ExportQuestion
    {
        public string Text { get; set; }

        public QuestionType Type { get; set; }

        public QuestionComplexityType Complexity { get; set; }

        public string Image { get; set; }

        public int Time { get; set; }

        public List<ExportAnswer> Answers { get; set; }
    }
}