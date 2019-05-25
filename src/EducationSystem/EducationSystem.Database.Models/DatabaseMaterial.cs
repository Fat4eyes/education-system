using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using EducationSystem.Database.Models.Basics;

namespace EducationSystem.Database.Models
{
    [Table("material")]
    public class DatabaseMaterial : DatabaseModel
    {
        [Column("owner_id")]
        public virtual int? OwnerId { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public virtual DatabaseUser Owner { get; set; }

        [Column("name")]
        public virtual string Name { get; set; }

        [Column("template")]
        public virtual string Template { get; set; }

        public virtual List<DatabaseMaterialFile> Files { get; set; }
            = new List<DatabaseMaterialFile>();

        public virtual List<DatabaseMaterialAnchor> Anchors { get; set; }
            = new List<DatabaseMaterialAnchor>();
    }
}