using EducationSystem.Models.Rest.Basics;

namespace EducationSystem.Models.Rest
{
    public class Group : Model
    {
        public string Name { get; set; }

        public int? Year { get; set; }

        public int StudyPlanId { get; set; }

        public StudyPlan StudyPlan { get; set; }
    }
}