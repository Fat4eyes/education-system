namespace EducationSystem.Models.Filters
{
    public class FilterTheme : Filter
    {
        public int? TestId { get; set; }

        public int? DisciplineId { get; set; }

        public FilterTheme SetTestId(int id)
        {
            TestId = id;
            return this;
        }

        public FilterTheme SetDisciplineId(int id)
        {
            DisciplineId = id;
            return this;
        }
    }
}