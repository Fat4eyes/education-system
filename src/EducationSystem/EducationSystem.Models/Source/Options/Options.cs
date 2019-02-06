namespace EducationSystem.Models.Source.Options
{
    public class Options
    {
        public bool All { get; set; } = false;

        public int Skip { get; set; } = 0;

        public int Take { get; set; } = 10;
    }
}