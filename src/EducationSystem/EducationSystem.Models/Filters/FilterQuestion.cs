namespace EducationSystem.Models.Filters
{
    public class FilterQuestion : Filter
    {
        public int? ThemeId { get; set; } = null;

        public FilterQuestion SetThemeId(int id)
        {
            ThemeId = id;

            return this;
        }
    }
}