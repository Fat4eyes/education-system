using EducationSystem.Models.Source.Rest.Base;

namespace EducationSystem.Models.Source.Rest
{
    public class Answer : Model
    {
        public string Text { get; set; }

        public bool IsRight { get; set; }
    }
}