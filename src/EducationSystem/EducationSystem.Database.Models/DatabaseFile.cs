using System.ComponentModel.DataAnnotations.Schema;
using EducationSystem.Database.Models.Basics;
using EducationSystem.Enums;

namespace EducationSystem.Database.Models
{
    [Table("file")]
    public class DatabaseFile : DatabaseModel
    {
        [Column("guid")]
        public virtual string Guid { get; set; }

        [Column("name")]
        public virtual string Name { get; set; }

        [Column("type")]
        public virtual FileType Type { get; set; }
    }
}