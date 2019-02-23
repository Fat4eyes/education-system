namespace EducationSystem.Models.Source.Options
{
    public class OptionsGroup : Options
    {
        public bool WithStudyPlan { get; set; } = false;

        public static OptionsGroup IncludeStudyPlan =>
            new OptionsGroup { WithStudyPlan = true };
    }
}