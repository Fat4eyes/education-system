namespace EducationSystem.Models.Source.Options
{
    public class OptionsUser : Options
    {
        public bool WithGroup { get; set; } = false;

        public bool WithRoles { get; set; } = false;

        public bool WithTestResults { get; set; } = false;

        public static OptionsUser IncludeRoles =>
            new OptionsUser { WithRoles = true };
    }
}