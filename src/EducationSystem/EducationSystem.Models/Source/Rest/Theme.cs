using EducationSystem.Models.Source.Rest.Base;

namespace EducationSystem.Models.Source.Rest
{
    public class Theme : Model
    {
        public Discipline Discipline { get; set; }

        public string Name { get; set; }
    }
}