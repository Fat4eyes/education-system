using System.Collections.Generic;
using EducationSystem.Models.Source.Rest.Base;

namespace EducationSystem.Models.Source.Rest
{
    public class Question : Model
    {
        public string Text { get; set; }

        public int Time { get; set; }

        public int ThemeId { get; set; }

        public List<Answer> Answers { get; set; }
    }
}