using EducationSystem.Models.Rest.Basics;

namespace EducationSystem.Models.Rest
{
    public class StudyProfile : Model
    {
        public string Name { get; set; }

        public int InstituteId { get; set; }

        public Institute Institute { get; set; }
    }
}