using EducationSystem.Models.Rest.Basics;

namespace EducationSystem.Models.Rest
{
    public class StudyPlan : Model
    {
        public string Name { get; set; }

        public int? Year { get; set; }

        public int StudyProfileId { get; set; }

        public StudyProfile StudyProfile { get; set; }
    }
}