﻿using System.Collections.Generic;
using EducationSystem.Enums;

namespace EducationSystem.Models.Filters
{
    public class FilterQuestion : Filter
    {
        public List<QuestionType> QuestionTypes { get; set; } = new List<QuestionType>
        {
            QuestionType.ClosedOneAnswer,
            QuestionType.ClosedManyAnswers,
            QuestionType.OpenedOneString,
            QuestionType.WithProgram
        };

        public TestSize TestSize { get; set; } = TestSize.S;
    }
}