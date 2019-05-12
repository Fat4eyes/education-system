namespace EducationSystem.Models.Filters
{
    public class FilterQuestion : Filter
    {
        public int? ThemeId { get; set; }

        public int? TestId { get; set; }

        public bool? Passed { get; set; } = null;

        public FilterQuestion SetThemeId(int id)
        {
            ThemeId = id;
            return this;
        }

        public FilterQuestion SetTestId(int id)
        {
            TestId = id;
            return this;
        }
    }
}