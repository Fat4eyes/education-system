namespace EducationSystem.Models.Options
{
    public class OptionsStudent : OptionsUser
    {
        public bool WithGroup { get; set; } = false;

        public bool WithTestResults { get; set; } = false;
    }
}