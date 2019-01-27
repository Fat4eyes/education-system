namespace EducationSystem.Models.Source
{
    /// <summary>
    /// Группа.
    /// </summary>
    public class Group
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Учебный план.
        /// </summary>
        public StudyPlan StudyPlan { get; set; }

        /// <summary>
        /// Префикс.
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// Курс (номер курса).
        /// </summary>
        public int Course { get; set; }

        /// <summary>
        /// Номер.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Признак того, что группа является очной.
        /// </summary>
        public bool IsFullTime { get; set; }

        /// <summary>
        /// Название (наименование).
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Год, с которого группа существует.
        /// </summary>
        public int? Year { get; set; }
    }
}