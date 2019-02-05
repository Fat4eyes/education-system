using EducationSystem.Models.Source.Rest.Base;

namespace EducationSystem.Models.Source.Rest
{
    public class Group : Model
    {
        public StudyPlan StudyPlan { get; set; }

        public string Prefix { get; set; }

        public int Course { get; set; }

        public int Number { get; set; }

        public bool IsFullTime { get; set; }

        public string Name { get; set; }

        public int? Year { get; set; }
    }
}