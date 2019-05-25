using System.ComponentModel.DataAnnotations.Schema;
using EducationSystem.Database.Models.Basics;

namespace EducationSystem.Database.Models
{
    [Table("material_anchor")]
    public class DatabaseMaterialAnchor : DatabaseModel
    {
        [Column("name")]
        public virtual string Name { get; set; }

        [Column("token")]
        public virtual string Token { get; set; }

        [Column("material_id")]
        public virtual int MaterialId { get; set; }

        [ForeignKey(nameof(MaterialId))]
        public virtual DatabaseMaterial Material { get; set; }
    }
}