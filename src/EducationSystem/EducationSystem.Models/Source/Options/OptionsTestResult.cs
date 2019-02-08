namespace EducationSystem.Models.Source.Options
{
    public class OptionsTestResult : Options
    {
        public int? ProfileId { get; set; } = null;

        public int? DisciplineId { get; set; } = null;

        public int? GroupId { get; set; } = null;

        public int? TestId { get; set; } = null;

        public bool WithTest { get; set; } = false;

        public bool WithThemes { get; set; } = false;

        public bool WithGivenAnswers { get; set; } = false;
    }
}