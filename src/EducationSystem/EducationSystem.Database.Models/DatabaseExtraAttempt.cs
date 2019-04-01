using System.ComponentModel.DataAnnotations.Schema;
using EducationSystem.Database.Models.Basics;

namespace EducationSystem.Database.Models
{
    [Table("extra_attempt")]
    public class DatabaseExtraAttempt : DatabaseModel
    {
        [Column("user_id")]
        public virtual int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual DatabaseUser User { get; set; }

        [Column("test_id")]
        public virtual int TestId { get; set; }

        [ForeignKey(nameof(TestId))]
        public virtual DatabaseTest Test { get; set; }

        public virtual int Count { get; set; }
    }
}