﻿using System.Collections.Generic;
using EducationSystem.Enums;
using EducationSystem.Models.Files;
using EducationSystem.Models.Rest.Basics;

namespace EducationSystem.Models.Rest
{
    public class Question : Model
    {
        public string Text { get; set; }

        public QuestionType? Type { get; set; }

        public QuestionComplexityType? Complexity { get; set; }

        public Image Image { get; set; }

        public int? Time { get; set; }

        public int? ThemeId { get; set; }

        public int? Order { get; set; }

        public Program Program { get; set; }

        public int? MaterialId { get; set; }

        public Material Material { get; set; }

        public List<Answer> Answers { get; set; }

        public Question Format()
        {
            Text = Text.Trim();

            Answers?.ForEach(x => x.Text = x.Text.Trim());

            if (Program == null)
                return this;

            Program.Template = Program.Template?.Trim();

            if (string.IsNullOrWhiteSpace(Program.Template))
                Program.Template = null;

            return this;
        }
    }
}