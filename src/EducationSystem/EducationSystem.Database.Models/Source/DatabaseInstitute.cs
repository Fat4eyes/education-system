using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using EducationSystem.Database.Models.Source.Base;

namespace EducationSystem.Database.Models.Source
{
    [Table("institute")]
    public class DatabaseInstitute : DatabaseModel
    {
        [Column("name")]
        public virtual string Name { get; set; }

        [Column("description")]
        public virtual string Description { get; set; }

        public virtual List<DatabaseStudyProfile> StudyProfiles { get; set; }
    }
}