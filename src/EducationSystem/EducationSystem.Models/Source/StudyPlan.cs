namespace EducationSystem.Models.Source
{
    /// <summary>
    /// Учебный план.
    /// </summary>
    public class StudyPlan
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор профиля.
        /// </summary>
        public int ProfileId { get; set; }

        /// <summary>
        /// Название (наименование).
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Год, с которого действует учебный план.
        /// </summary>
        public int? Year { get; set; }
    }
}