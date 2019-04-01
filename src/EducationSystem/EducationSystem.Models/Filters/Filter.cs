namespace EducationSystem.Models.Filters
{
    public class Filter
    {
        public bool All { get; set; } = false;

        public int Skip { get; set; } = 0;

        public int Take { get; set; } = 10;
    }
}