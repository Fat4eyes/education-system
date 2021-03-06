﻿using System.ComponentModel.DataAnnotations.Schema;
using EducationSystem.Database.Models.Basics;

namespace EducationSystem.Database.Models
{
    [Table("student_group")]
    public class DatabaseStudentGroup : DatabaseModel
    {
        [Column("student_id")]
        public virtual int StudentId { get; set; }

        [ForeignKey(nameof(StudentId))]
        public virtual DatabaseUser Student { get; set; }

        [Column("group_id")]
        public virtual int GroupId { get; set; }

        [ForeignKey(nameof(StudentId))]
        public virtual DatabaseGroup Group { get; set; }
    }
}