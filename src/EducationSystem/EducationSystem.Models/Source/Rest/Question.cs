﻿using System.Collections.Generic;
using EducationSystem.Enums.Source;
using EducationSystem.Models.Source.Rest.Base;

namespace EducationSystem.Models.Source.Rest
{
    public class Question : Model
    {
        public string Text { get; set; }

        public QuestionType? Type { get; set; }

        public QuestionComplexityType? Complexity { get; set; }

        public int? Time { get; set; }

        public int ThemeId { get; set; }

        public Program Program { get; set; }

        public List<Answer> Answers { get; set; }
    }
}