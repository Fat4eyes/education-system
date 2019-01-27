using EducationSystem.Models.Source.Base;

namespace EducationSystem.Models.Source
{
    /// <summary>
    /// Профиль обучения.
    /// </summary>
    public class StudyProfile : Model
    {
        /// <summary>
        /// Институт.
        /// </summary>
        public Institute Institute { get; set; }

        /// <summary>
        /// Код.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Название (наименование).
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Полное название (наименование).
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Количество семестров.
        /// </summary>
        public int SemestersCount { get; set; }
    }
}