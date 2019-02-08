using System.ComponentModel.DataAnnotations.Schema;

namespace EducationSystem.Database.Models.Source
{
    [Table("profile_discipline")]
    public class DatabaseStudyProfileDiscipline
    {
        [Column("discipline_id")]
        public int DisciplineId { get; set; }

        [ForeignKey(nameof(DisciplineId))]
        public DatabaseDiscipline Discipline { get; set; }

        [Column("profile_id")]
        public int StudyProfileId { get; set; }

        [ForeignKey(nameof(StudyProfileId))]
        public DatabaseStudyProfile StudyProfile { get; set; }
    }
}