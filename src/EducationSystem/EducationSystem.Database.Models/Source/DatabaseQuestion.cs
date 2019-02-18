﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using EducationSystem.Database.Models.Source.Base;
using EducationSystem.Enums.Source;

namespace EducationSystem.Database.Models.Source
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

        [Column("complexity")]
        public virtual QuestionComplexityType Complexity { get; set; }

        [Column("time")]
        public virtual int Time { get; set; }

        public virtual DatabaseProgram Program { get; set; }

        public virtual List<DatabaseAnswer> Answers { get; set; }

        public virtual List<DatabaseGivenAnswer> GivenAnswers { get; set; }
    }
}