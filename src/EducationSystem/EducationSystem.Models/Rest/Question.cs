using System.Collections.Generic;
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

        public List<Answer> Answers { get; set; } = new List<Answer>();

        public int? TestId { get; set; }

        public string Hash { get; set; }

        public bool? Right { get; set; }

        public Question SetTestId(int testId)
        {
            TestId = testId;
            return this;
        }

        public Question SetRight(bool right)
        {
            Right = right;
            return this;
        }

        public Question Format()
        {
            Text = Text?.Trim();

            Answers?.ForEach(x => x.Text = x.Text?.Trim());

            if (Program == null)
                return this;

            Program.Template = Program.Template?.Trim();

            if (string.IsNullOrWhiteSpace(Program.Template))
                Program.Template = null;

            return this;
        }
    }
}