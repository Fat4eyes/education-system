namespace EducationSystem.Models.Source.Options
{
    public class OptionsGroup : Options
    {
        public string Name { get; set; } = null;

        public bool WithStudyPlan { get; set; } = false;
    }
}