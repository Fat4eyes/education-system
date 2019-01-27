using EducationSystem.Models.Source.Base;

namespace EducationSystem.Models.Source
{
    /// <summary>
    /// Доменная модель: Институт.
    /// </summary>
    public class Institute : Model
    {
        /// <summary>
        /// Название (наименование).
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; set; }
    }
}