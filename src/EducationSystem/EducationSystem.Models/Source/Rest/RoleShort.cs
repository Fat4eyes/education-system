using EducationSystem.Models.Source.Base;

namespace EducationSystem.Models.Source.Rest
{
    /// <summary>
    /// Роль пользователя.
    /// </summary>
    public class RoleShort : Model
    {
        /// <summary>
        /// Название.
        /// </summary>
        public string Name { get; set; }
    }
}