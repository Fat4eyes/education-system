using System;
using System.ComponentModel.DataAnnotations.Schema;
using EducationSystem.Database.Models.Basics;
using EducationSystem.Enums;

namespace EducationSystem.Database.Models
{
    [Table("file")]
    public class DatabaseFile : DatabaseModel
    {
        [Column("guid")]
        public virtual Guid Guid { get; set; }

        [Column("name")]
        public virtual string Name { get; set; }

        [Column("type")]
        public virtual FileType Type { get; set; }

        [Column("owner_id")]
        public virtual int? OwnerId { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public virtual DatabaseUser Owner { get; set; }
    }
}