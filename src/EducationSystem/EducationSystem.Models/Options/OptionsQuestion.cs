namespace EducationSystem.Models.Options
{
    public class OptionsQuestion : Options
    {
        public bool WithAnswers { get; set; } = false;

        public bool WithProgram { get; set; } = false;

        public bool WithMaterial { get; set; } = false;
    }
}