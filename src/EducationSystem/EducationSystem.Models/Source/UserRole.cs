using EducationSystem.Models.Source.Base;

namespace EducationSystem.Models.Source
{
    /// <summary>
    /// Роль (пользователя).
    /// </summary>
    public class UserRole : Model
    {
        /// <summary>
        /// Название.
        /// </summary>
        public string Name { get; set; }

        public string Slug { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Уровень.
        /// </summary>
        public int Level { get; set; }
    }
}