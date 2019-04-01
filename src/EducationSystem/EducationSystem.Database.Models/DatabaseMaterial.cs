using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using EducationSystem.Database.Models.Basics;

namespace EducationSystem.Database.Models
{
    [Table("material")]
    public class DatabaseMaterial : DatabaseModel
    {
        [Column("name")]
        public virtual string Name { get; set; }

        [Column("template")]
        public virtual string Template { get; set; }

        public virtual List<DatabaseMaterialFile> Files { get; set; }
            = new List<DatabaseMaterialFile>();
    }
}