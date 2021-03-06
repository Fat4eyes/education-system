﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using EducationSystem.Database.Models.Basics;

namespace EducationSystem.Database.Models
{
    [Table("studyplan")]
    public class DatabaseStudyPlan : DatabaseModel
    {
        [Column("profile_id")]
        public virtual int StudyProfileId { get; set; }

        [ForeignKey(nameof(StudyProfileId))]
        public virtual DatabaseStudyProfile StudyProfile { get; set; }

        [Column("name")]
        public virtual string Name { get; set; }

        [Column("year")]
        public virtual int? Year { get; set; }

        public virtual List<DatabaseGroup> Groups { get; set; }
            = new List<DatabaseGroup>();
    }
}