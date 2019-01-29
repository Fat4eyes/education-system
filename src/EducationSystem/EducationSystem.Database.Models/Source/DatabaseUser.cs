using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using EducationSystem.Database.Models.Source.Base;

namespace EducationSystem.Database.Models.Source
{
    /// <summary>
    /// Доменная модель: Пользователь.
    /// </summary>
    [Table("user")]
    public class DatabaseUser : DatabaseModel
    {
        /// <summary>
        /// Имя.
        /// </summary>
        [Column("firstname")]
        public virtual string FirstName { get; set; }

        /// <summary>
        /// Отчество.
        /// </summary>
        [Column("patronymic")]
        public virtual string MiddleName { get; set; }

        /// <summary>
        /// Фамилия.
        /// </summary>
        [Column("lastname")]
        public virtual string LastName { get; set; }

        /// <summary>
        /// E-Mail (электронная почта).
        /// </summary>
        [Column("email")]
        public virtual string Email { get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        [Column("password")]
        public virtual string Password { get; set; }

        /// <summary>
        /// Признак того, что пользователь активный (подтвержден).
        /// </summary>
        [Column("active")]
        public virtual int Active { get; set; }

        /// <summary>
        /// Роли.
        /// </summary>
        public virtual List<DatabaseUserRole> Roles { get; set; }
    }
}