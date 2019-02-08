using System.ComponentModel.DataAnnotations.Schema;
using EducationSystem.Database.Models.Source.Base;

namespace EducationSystem.Database.Models.Source
{
    [Table("given_answer")]
    public class DatabaseGivenAnswer : DatabaseModel
    {
        [Column("test_result_id")]
        public virtual int TestResultId { get; set; }

        [ForeignKey(nameof(TestResultId))]
        public virtual DatabaseTestResult TestResult { get; set; }

        [Column("question_id")]
        public virtual int QuestionId { get; set; }

        [ForeignKey(nameof(QuestionId))]
        public virtual DatabaseQuestion Question { get; set; }

        [Column("answer")]
        public virtual string Answer { get; set; }

        [Column("right_percentage")]
        public virtual int RightPercentage { get; set; }
    }
}