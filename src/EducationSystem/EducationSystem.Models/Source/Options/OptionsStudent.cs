namespace EducationSystem.Models.Source.Options
{
    public class OptionsStudent : OptionsUser
    {
        public bool WithGroup { get; set; } = false;

        public bool WithTestResults { get; set; } = false;
    }
}