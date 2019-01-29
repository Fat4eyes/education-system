using System.Collections.Generic;
using EducationSystem.Models.Source.Base;

namespace EducationSystem.Models.Source
{
    /// <summary>
    /// Пользователь.
    /// </summary>
    public class User : Model
    {
        /// <summary>
        /// Имя.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Отчество.
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Фамилия.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// E-Mail (электронная почта).
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Признак того, что пользователь активный (подтвержден).
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Роли.
        /// </summary>
        public List<UserRole> Roles { get; set; }
    }
}