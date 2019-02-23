namespace EducationSystem.Models.Source.Options
{
    public class OptionsQuestion : Options
    {
        public bool WithAnswers { get; set; } = false;

        public bool WithProgram { get; set; } = false;
    }
}