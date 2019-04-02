using EducationSystem.Models.Rest.Basics;

namespace EducationSystem.Models.Rest
{
    public class GivenAnswer : Model
    {
        public int TestResultId { get; set; }

        public string Answer { get; set; }

        public int RightPercentage { get; set; }

        public int QuestionId { get; set; }

        public Question Question { get; set; }
    }
}