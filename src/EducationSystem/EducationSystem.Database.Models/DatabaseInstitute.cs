using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using EducationSystem.Database.Models.Basics;

namespace EducationSystem.Database.Models
{
    [Table("institute")]
    public class DatabaseInstitute : DatabaseModel
    {
        [Column("name")]
        public virtual string Name { get; set; }

        public virtual List<DatabaseStudyProfile> StudyProfiles { get; set; }
            = new List<DatabaseStudyProfile>();
    }
}