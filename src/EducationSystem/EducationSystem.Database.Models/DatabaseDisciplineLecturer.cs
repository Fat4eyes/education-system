using System.ComponentModel.DataAnnotations.Schema;
using EducationSystem.Database.Models.Basics;

namespace EducationSystem.Database.Models
{
    [Table("discipline_lecturer")]
    public class DatabaseDisciplineLecturer : DatabaseModel
    {
        [Column("lecturer_id")]
        public virtual int LecturerId { get; set; }

        [ForeignKey(nameof(LecturerId))]
        public virtual DatabaseUser Lecturer { get; set; }

        [Column("discipline_id")]
        public virtual int DisciplineId { get; set; }

        [ForeignKey(nameof(DisciplineId))]
        public virtual DatabaseDiscipline Discipline { get; set; }
    }
}