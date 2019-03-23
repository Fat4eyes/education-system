using System.ComponentModel.DataAnnotations.Schema;
using EducationSystem.Database.Models.Source.Base;
using EducationSystem.Enums.Source;

namespace EducationSystem.Database.Models.Source
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