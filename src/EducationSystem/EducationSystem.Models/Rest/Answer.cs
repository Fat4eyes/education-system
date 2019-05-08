using EducationSystem.Enums;
using EducationSystem.Models.Rest.Basics;

namespace EducationSystem.Models.Rest
{
    public class Answer : Model
    {
        public string Text { get; set; }

        public bool? IsRight { get; set; }

        public int? QuestionId { get; set; }

        public AnswerStatus? Status { get; set; }
    }
}