using System.ComponentModel.DataAnnotations.Schema;
using EducationSystem.Database.Models.Source.Base;

namespace EducationSystem.Database.Models.Source
{
    /// <summary>
    /// Доменная модель: Роль пользователя.
    /// </summary>
    [Table("roles")]
    public class DatabaseRole : DatabaseModel
    {
        /// <summary>
        /// Название.
        /// </summary>
        [Column("name")]
        public virtual string Name { get; set; }

        [Column("slug")]
        public virtual string Slug { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        [Column("description")]
        public virtual string Description { get; set; }

        /// <summary>
        /// Уровень.
        /// </summary>
        [Column("level")]
        public virtual int Level { get; set; }
    }
}