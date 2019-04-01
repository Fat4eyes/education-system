using System.ComponentModel.DataAnnotations.Schema;

namespace EducationSystem.Database.Models
{
    [Table("profile_discipline")]
    public class DatabaseStudyProfileDiscipline
    {
        [Column("discipline_id")]
        public virtual int DisciplineId { get; set; }

        [ForeignKey(nameof(DisciplineId))]
        public virtual DatabaseDiscipline Discipline { get; set; }

        [Column("profile_id")]
        public virtual int StudyProfileId { get; set; }

        [ForeignKey(nameof(StudyProfileId))]
        public virtual DatabaseStudyProfile StudyProfile { get; set; }
    }
}