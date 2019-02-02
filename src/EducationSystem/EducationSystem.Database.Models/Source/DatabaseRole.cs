using System.ComponentModel.DataAnnotations.Schema;
using EducationSystem.Database.Models.Source.Base;

namespace EducationSystem.Database.Models.Source
{
    [Table("roles")]
    public class DatabaseRole : DatabaseModel
    {
        [Column("name")]
        public virtual string Name { get; set; }

        [Column("slug")]
        public virtual string Slug { get; set; }

        [Column("description")]
        public virtual string Description { get; set; }

        [Column("level")]
        public virtual int Level { get; set; }
    }
}