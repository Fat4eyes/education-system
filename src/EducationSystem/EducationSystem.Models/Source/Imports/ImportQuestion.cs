using System.Collections.Generic;
using EducationSystem.Enums.Source;
using EducationSystem.Models.Source.Exports;

namespace EducationSystem.Models.Source.Imports
{
    public class ImportQuestion
    {
        public string Text { get; set; }

        public QuestionType? Type { get; set; }

        public QuestionComplexityType? Complexity { get; set; }

        public string Image { get; set; }

        public int? Time { get; set; }

        public List<ExportAnswer> Answers { get; set; }
    }
}