using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using EducationSystem.Database.Models.Source.Base;

namespace EducationSystem.Database.Models.Source
{
    [Table("profile")]
    public class DatabaseStudyProfile : DatabaseModel
    {
        [Column("institute_id")]
        public virtual int InstituteId { get; set; }

        [ForeignKey(nameof(InstituteId))]
        public virtual DatabaseInstitute Institute { get; set; }

        [Column("code")]
        public virtual string Code { get; set; }

        [Column("name")]
        public virtual string Name { get; set; }

        [Column("fullname")]
        public virtual string FullName { get; set; }

        [Column("semesters")]
        public virtual int SemestersCount { get; set; }

        public virtual List<DatabaseStudyPlan> StudyPlans { get; set; }
    }
}