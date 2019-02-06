using System.ComponentModel.DataAnnotations.Schema;
using EducationSystem.Database.Models.Source.Base;

namespace EducationSystem.Database.Models.Source
{
    [Table("answer")]
    public class DatabaseAnswer : DatabaseModel
    {
        [Column("question_id")]
        public virtual int QuestionId { get; set; }

        [ForeignKey(nameof(QuestionId))]
        public virtual DatabaseQuestion Question { get; set; }

        [Column("text")]
        public virtual string Text { get; set; }

        [Column("is_right")]
        public virtual int IsRight { get; set; }
    }
}