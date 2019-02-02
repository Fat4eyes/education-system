﻿using System.ComponentModel.DataAnnotations.Schema;
using EducationSystem.Database.Models.Source.Base;

namespace EducationSystem.Database.Models.Source
{
    [Table("group")]
    public class DatabaseGroup : DatabaseModel
    {
        [Column("studyplan_id")]
        public virtual int StudyPlanId { get; set; }

        [ForeignKey(nameof(StudyPlanId))]
        public virtual DatabaseStudyPlan StudyPlan { get; set; }

        [Column("prefix")]
        public virtual string Prefix { get; set; }

        [Column("course")]
        public virtual int Course { get; set; }

        [Column("number")]
        public virtual int Number { get; set; }

        [Column("is_fulltime")]
        public virtual int IsFullTime { get; set; }

        [Column("name")]
        public virtual string Name { get; set; }

        [Column("year")]
        public virtual int? Year { get; set; }
    }
}