using System.ComponentModel.DataAnnotations.Schema;
using EducationSystem.Database.Models.Source.Base;

namespace EducationSystem.Database.Models.Source
{
    /// <summary>
    /// Доменная модель: Профиль обучения.
    /// </summary>
    [Table("profile")]
    public class DatabaseStudyProfile : DatabaseModel
    {
        /// <summary>
        /// Идентификатор института.
        /// </summary>
        [Column("institute_id")]
        public virtual int InstituteId { get; set; }

        /// <summary>
        /// Институт.
        /// </summary>
        [ForeignKey(nameof(InstituteId))]
        public virtual DatabaseInstitute Institute { get; set; }

        /// <summary>
        /// Код.
        /// </summary>
        [Column("code")]
        public virtual string Code { get; set; }

        /// <summary>
        /// Название (наименование).
        /// </summary>
        [Column("name")]
        public virtual string Name { get; set; }

        /// <summary>
        /// Полное название (наименование).
        /// </summary>
        [Column("fullname")]
        public virtual string FullName { get; set; }

        /// <summary>
        /// Количество семестров.
        /// </summary>
        [Column("semesters")]
        public virtual int SemestersCount { get; set; }
    }
}