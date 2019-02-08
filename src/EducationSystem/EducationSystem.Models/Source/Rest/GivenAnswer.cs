using EducationSystem.Models.Source.Rest.Base;

namespace EducationSystem.Models.Source.Rest
{
    public class GivenAnswer : Model
    {
        public int TestResultId { get; set; }

        public int QuestionId { get; set; }

        public Question Question { get; set; }

        public string Answer { get; set; }

        public int RightPercentage { get; set; }
    }
}