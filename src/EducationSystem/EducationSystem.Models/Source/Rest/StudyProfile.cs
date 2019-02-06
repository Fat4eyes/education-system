using EducationSystem.Models.Source.Rest.Base;

namespace EducationSystem.Models.Source.Rest
{
    public class StudyProfile : Model
    {
        public string Name { get; set; }

        public Institute Institute { get; set; }
    }
}