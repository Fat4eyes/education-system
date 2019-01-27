using System.ComponentModel.DataAnnotations.Schema;
using EducationSystem.Database.Models.Source.Base;

namespace EducationSystem.Database.Models.Source
{
    /// <summary>
    /// Доменная модель: Учебный план.
    /// </summary>
    [Table("studyplan")]
    public class DatabaseStudyPlan : DatabaseModel
    {
        /// <summary>
        /// Идентификатор профиля.
        /// </summary>
        [Column("profile_id")]
        public virtual int StudyProfileId { get; set; }

        /// <summary>
        /// Профиль обучения.
        /// </summary>
        [ForeignKey(nameof(StudyProfileId))]
        public virtual DatabaseStudyProfile StudyProfile { get; set; }

        /// <summary>
        /// Название (наименование).
        /// </summary>
        [Column("name")]
        public virtual string Name { get; set; }

        /// <summary>
        /// Год.
        /// </summary>
        [Column("year")]
        public virtual int? Year { get; set; }
    }
}