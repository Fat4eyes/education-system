using System.ComponentModel.DataAnnotations.Schema;

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
        public virtual int ProfileId { get; set; }

        /// <summary>
        /// Название (наименование).
        /// </summary>
        [Column("name")]
        public virtual string Name { get; set; }

        /// <summary>
        /// Год.
        /// </summary>
        [Column("year")]
        public int? Year { get; set; }
    }
}