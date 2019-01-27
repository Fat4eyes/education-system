using System.ComponentModel.DataAnnotations.Schema;
using EducationSystem.Database.Models.Source.Base;

namespace EducationSystem.Database.Models.Source
{
    /// <summary>
    /// Доменная модель: Группа.
    /// </summary>
    [Table("group")]
    public class DatabaseGroup : DatabaseModel
    {
        /// <summary>
        /// Идентификатор учебного плана.
        /// </summary>
        [Column("studyplan_id")]
        public virtual int StudyPlanId { get; set; }

        /// <summary>
        /// Учебный план.
        /// </summary>
        [ForeignKey(nameof(StudyPlanId))]
        public virtual DatabaseStudyPlan StudyPlan { get; set; }

        /// <summary>
        /// Префикс.
        /// </summary>
        [Column("prefix")]
        public virtual string Prefix { get; set; }

        /// <summary>
        /// Курс (номер курса).
        /// </summary>
        [Column("course")]
        public virtual int Course { get; set; }

        /// <summary>
        /// Номер.
        /// </summary>
        [Column("number")]
        public virtual int Number { get; set; }

        /// <summary>
        /// Признак того, что группа является очной.
        /// </summary>
        [Column("is_fulltime")]
        public virtual int IsFullTime { get; set; }

        /// <summary>
        /// Название (наименование).
        /// </summary>
        [Column("name")]
        public virtual string Name { get; set; }

        /// <summary>
        /// Год, с которого группа существует.
        /// </summary>
        [Column("year")]
        public virtual int? Year { get; set; }
    }
}