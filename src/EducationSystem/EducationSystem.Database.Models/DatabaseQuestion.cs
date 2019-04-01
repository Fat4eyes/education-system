﻿using System.Collections.Generic;
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

        public virtual List<DatabaseGivenAnswer> GivenAnswers { get; set; }
            = new List<DatabaseGivenAnswer>();
    }
}