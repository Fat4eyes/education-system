using System.ComponentModel.DataAnnotations.Schema;
using EducationSystem.Database.Models.Basics;

namespace EducationSystem.Database.Models
{
    [Table("material_file")]
    public class DatabaseMaterialFile : DatabaseModel
    {
        [Column("material_id")]
        public virtual int MaterialId { get; set; }

        [ForeignKey(nameof(MaterialId))]
        public virtual DatabaseMaterial Material { get; set; }

        [Column("file_id")]
        public virtual int FileId { get; set; }

        [ForeignKey(nameof(FileId))]
        public virtual DatabaseFile File { get; set; }
    }
}