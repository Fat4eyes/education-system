using System.ComponentModel.DataAnnotations.Schema;
using EducationSystem.Database.Models.Basics;

namespace EducationSystem.Database.Models
{
    [Table("question_material_anchor")]
    public class DatabaseQuestionMaterialAnchor : DatabaseModel
    {
        [Column("question_id")]
        public virtual int QuestionId { get; set; }

        [ForeignKey(nameof(QuestionId))]
        public virtual DatabaseQuestion Question { get; set; }

        [Column("material_anchor_id")]
        public virtual int MaterialAnchorId { get; set; }

        [ForeignKey(nameof(MaterialAnchorId))]
        public virtual DatabaseMaterialAnchor MaterialAnchor { get; set; }
    }
}