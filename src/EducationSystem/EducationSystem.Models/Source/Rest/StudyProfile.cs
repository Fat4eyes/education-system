using EducationSystem.Models.Source.Rest.Base;

namespace EducationSystem.Models.Source.Rest
{
    public class StudyProfile : Model
    {
        public Institute Institute { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public int SemestersCount { get; set; }
    }
}