using EducationSystem.Models.Source.Rest.Base;

namespace EducationSystem.Models.Source.Rest
{
    public class Discipline : Model
    {
        public string Name { get; set; }

        public string Abbreviation { get; set; }

        public string Description { get; set; }
    }
}