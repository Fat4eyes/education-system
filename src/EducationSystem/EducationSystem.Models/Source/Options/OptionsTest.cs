namespace EducationSystem.Models.Source.Options
{
    public class OptionsTest : Options
    {
        public string Name { get; set; } = null;

        public int? DisciplineId { get; set; } = null;

        public bool OnlyActive { get; set; } = false;

        public bool WithThemes { get; set; } = false;
    }
}