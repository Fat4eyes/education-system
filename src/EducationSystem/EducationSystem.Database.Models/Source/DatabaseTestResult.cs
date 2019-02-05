using System;
using System.ComponentModel.DataAnnotations.Schema;
using EducationSystem.Database.Models.Source.Base;

namespace EducationSystem.Database.Models.Source
{
    [Table("test_result")]
    public class DatabaseTestResult : DatabaseModel
    {
        [Column("test_id")]
        public virtual int TestId { get; set; }

        [ForeignKey(nameof(TestId))]
        public virtual DatabaseTest Test { get; set; }

        [Column("user_id")]
        public virtual int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual DatabaseUser User { get; set; }

        [Column("attempt")]
        public virtual int Attempt { get; set; }

        [Column("mark")]
        public virtual int Mark { get; set; }

        [Column("date_time")]
        public virtual DateTime DateTime { get; set; }
    }
}