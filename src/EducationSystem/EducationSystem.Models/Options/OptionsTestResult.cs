namespace EducationSystem.Models.Options
{
    public class OptionsTestResult : Options
    {
        public bool WithTest { get; set; } = false;

        public bool WithGivenAnswers { get; set; } = false;
    }
}