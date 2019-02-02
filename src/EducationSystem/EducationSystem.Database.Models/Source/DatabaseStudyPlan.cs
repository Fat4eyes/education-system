using System.ComponentModel.DataAnnotations.Schema;
using EducationSystem.Database.Models.Source.Base;

namespace EducationSystem.Database.Models.Source
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
    }
}