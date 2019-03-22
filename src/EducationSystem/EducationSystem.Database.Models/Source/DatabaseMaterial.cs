using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using EducationSystem.Database.Models.Source.Base;

namespace EducationSystem.Database.Models.Source
{
    [Table("material")]
    public class DatabaseMaterial : DatabaseModel
    {
        [Column("name")]
        public virtual string Name { get; set; }

        [Column("template")]
        public virtual string Tempalate { get; set; }

        public virtual List<DatabaseMaterialFile> Files { get; set; }
            = new List<DatabaseMaterialFile>();
    }
}