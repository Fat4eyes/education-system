﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using EducationSystem.Database.Models.Basics;
using EducationSystem.Enums;

namespace EducationSystem.Database.Models
{
    [Table("program")]
    public class DatabaseProgram : DatabaseModel
    {
        [Column("question_id")]
        public virtual int QuestionId { get; set; }

        [ForeignKey(nameof(QuestionId))]
        public virtual DatabaseQuestion Question { get; set; }

        [Column("template")]
        public virtual string Template { get; set; }

        [Column("lang")]
        public virtual LanguageType LanguageType { get; set; }

        [Column("time_limit")]
        public virtual int TimeLimit { get; set; }

        [Column("memory_limit")]
        public virtual int MemoryLimit { get; set; }

        public virtual List<DatabaseProgramData> ProgramDatas { get; set; }
            = new List<DatabaseProgramData>();
    }
}