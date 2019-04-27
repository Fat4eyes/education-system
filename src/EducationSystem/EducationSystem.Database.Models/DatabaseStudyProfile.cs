using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using EducationSystem.Database.Models.Basics;

namespace EducationSystem.Database.Models
{
    [Table("profile")]
    public class DatabaseStudyProfile : DatabaseModel
    {
        [Column("institute_id")]
        public virtual int InstituteId { get; set; }

        [ForeignKey(nameof(InstituteId))]
        public virtual DatabaseInstitute Institute { get; set; }

        [Column("name")]
        public virtual string Name { get; set; }

        public virtual List<DatabaseStudyPlan> StudyPlans { get; set; }
            = new List<DatabaseStudyPlan>();

        public virtual List<DatabaseStudyProfileDiscipline> Disciplines { get; set; }
            = new List<DatabaseStudyProfileDiscipline>();
    }
}