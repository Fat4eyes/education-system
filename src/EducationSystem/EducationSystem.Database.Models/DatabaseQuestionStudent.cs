using System.ComponentModel.DataAnnotations.Schema;
using EducationSystem.Database.Models.Basics;

namespace EducationSystem.Database.Models
{
    [Table("question_student")]
    public class DatabaseQuestionStudent : DatabaseModel
    {
        [Column("student_id")]
        public virtual int StudentId { get; set; }

        [ForeignKey(nameof(StudentId))]
        public virtual DatabaseUser Student { get; set; }

        [Column("question_id")]
        public virtual int QuestionId { get; set; }

        [ForeignKey(nameof(QuestionId))]
        public virtual DatabaseQuestion Question { get; set; }

        [Column("passed")]
        public virtual bool Passed { get; set; }
    }
}