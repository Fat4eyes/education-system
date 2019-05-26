using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using EducationSystem.Database.Models.Basics;
using EducationSystem.Enums;

namespace EducationSystem.Database.Models
{
    [Table("question")]
    public class DatabaseQuestion : DatabaseModel
    {
        [Column("theme_id")]
        public virtual int ThemeId { get; set; }

        [ForeignKey(nameof(ThemeId))]
        public virtual DatabaseTheme Theme { get; set; }

        [Column("type")]
        public virtual QuestionType Type { get; set; }
        
        [Column("text")]
        public virtual string Text { get; set; }

        [Column("image_id")]
        public virtual int? ImageId { get; set; }

        [Column("order")]
        public virtual int? Order { get; set; }

        [ForeignKey(nameof(ImageId))]
        public virtual DatabaseFile Image { get; set; }

        [Column("material_id")]
        public virtual int? MaterialId { get; set; }

        [ForeignKey(nameof(MaterialId))]
        public virtual DatabaseMaterial Material { get; set; }

        [Column("complexity")]
        public virtual QuestionComplexityType Complexity { get; set; }

        [Column("time")]
        public virtual int Time { get; set; }

        public virtual DatabaseProgram Program { get; set; }

        public virtual List<DatabaseAnswer> Answers { get; set; }
            = new List<DatabaseAnswer>();

        public virtual List<DatabaseQuestionStudent> QuestionStudents { get; set; }
            = new List<DatabaseQuestionStudent>();

        public virtual List<DatabaseQuestionMaterialAnchor> MaterialAnchors { get; set; }
            = new List<DatabaseQuestionMaterialAnchor>();
    }
}