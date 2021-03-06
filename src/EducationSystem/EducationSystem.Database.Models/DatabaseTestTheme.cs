﻿using System.ComponentModel.DataAnnotations.Schema;
using EducationSystem.Database.Models.Basics;

namespace EducationSystem.Database.Models
{
    [Table("test_theme")]
    public class DatabaseTestTheme : DatabaseModel
    {
        [Column("test_id")]
        public virtual int TestId { get; set; }

        [ForeignKey(nameof(TestId))]
        public virtual DatabaseTest Test { get; set; }

        [Column("theme_id")]
        public virtual int ThemeId { get; set; }

        [ForeignKey(nameof(ThemeId))]
        public virtual DatabaseTheme Theme { get; set; }
    }
}