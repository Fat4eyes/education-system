﻿using System.Collections.Generic;
using EducationSystem.Enums;
using EducationSystem.Models.Rest.Basics;

namespace EducationSystem.Models.Rest
{
    public class Test : Model
    {
        public string Subject { get; set; }

        public bool IsActive { get; set; }

        public int DisciplineId { get; set; }

        public int? TotalTime { get; set; }

        public int? Attempts { get; set; }

        public TestType? Type { get; set; }

        public List<Theme> Themes { get; set; }

        public int? ThemesCount { get; set; }

        public int? QuestionsCount { get; set; }

        public int? PassedThemesCount { get; set; }

        public int? PassedQuestionsCount { get; set; }

        public Test Format()
        {
            Subject = Subject?.Trim();

            return this;
        }
    }
}