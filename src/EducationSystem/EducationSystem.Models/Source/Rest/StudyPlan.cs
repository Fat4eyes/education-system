using EducationSystem.Models.Source.Base;

namespace EducationSystem.Models.Source.Rest
{
    /// <summary>
    /// Учебный план.
    /// </summary>
    public class StudyPlan : Model
    {
        /// <summary>
        /// Профиль обучения.
        /// </summary>
        public StudyProfile StudyProfile { get; set; }

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