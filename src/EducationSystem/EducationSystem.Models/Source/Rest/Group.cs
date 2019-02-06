using EducationSystem.Models.Source.Rest.Base;

namespace EducationSystem.Models.Source.Rest
{
    public class Group : Model
    {
        public string Name { get; set; }

        public int? Year { get; set; }

        public StudyPlan StudyPlan { get; set; }
    }
}