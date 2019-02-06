using EducationSystem.Models.Source.Rest.Base;

namespace EducationSystem.Models.Source.Rest
{
    public class StudyPlan : Model
    {
        public string Name { get; set; }

        public int? Year { get; set; }

        public StudyProfile StudyProfile { get; set; }
    }
}