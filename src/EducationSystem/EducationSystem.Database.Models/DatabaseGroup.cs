using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using EducationSystem.Database.Models.Basics;

namespace EducationSystem.Database.Models
{
    [Table("group")]
    public class DatabaseGroup : DatabaseModel
    {
        [Column("studyplan_id")]
        public virtual int StudyPlanId { get; set; }

        [ForeignKey(nameof(StudyPlanId))]
        public virtual DatabaseStudyPlan StudyPlan { get; set; }

        [Column("name")]
        public virtual string Name { get; set; }

        [Column("year")]
        public virtual int? Year { get; set; }

        public virtual List<DatabaseStudentGroup> GroupStudents { get; set; }
            = new List<DatabaseStudentGroup>();
    }
}