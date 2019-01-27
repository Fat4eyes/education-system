using System.ComponentModel.DataAnnotations.Schema;
using EducationSystem.Database.Models.Source.Base;

namespace EducationSystem.Database.Models.Source
{
    /// <summary>
    /// Доменная модель: Институт.
    /// </summary>
    [Table("institute")]
    public class DatabaseInstitute : DatabaseModel
    {
        /// <summary>
        /// Название (наименование).
        /// </summary>
        [Column("name")]
        public virtual string Name { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        [Column("description")]
        public virtual string Description { get; set; }
    }
}