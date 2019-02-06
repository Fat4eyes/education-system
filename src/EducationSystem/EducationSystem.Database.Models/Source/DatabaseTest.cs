﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using EducationSystem.Database.Models.Source.Base;

namespace EducationSystem.Database.Models.Source
{
    [Table("test")]
    public class DatabaseTest : DatabaseModel
    {
        [Column("discipline_id")]
        public virtual int DisciplineId { get; set; }

        [ForeignKey(nameof(DisciplineId))]
        public virtual DatabaseDiscipline Discipline { get; set; }

        [Column("subject")]
        public virtual string Subject { get; set; }

        [Column("time_total")]
        public virtual int TotalTime { get; set; }

        [Column("attempts")]
        public virtual int Attempts { get; set; }

        [Column("type")]
        public virtual int Type { get; set; }

        [Column("is_active")]
        public virtual int IsActive { get; set; }

        [Column("is_random")]
        public virtual int IsRandom { get; set; }

        public virtual List<DatabaseTestTheme> TestThemes { get; set; }
    }
}