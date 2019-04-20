using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using EducationSystem.Database.Models.Basics;

namespace EducationSystem.Database.Models
{
    [Table("theme")]
    public class DatabaseTheme : DatabaseModel
    {
        [Column("name")]
        public virtual string Name { get; set; }

        [Column("discipline_id")]
        public virtual int DisciplineId { get; set; }

        [Column("order")]
        public virtual int? Order { get; set; }

        [ForeignKey(nameof(DisciplineId))]
        public virtual DatabaseDiscipline Discipline { get; set; }

        public virtual List<DatabaseTestTheme> ThemeTests { get; set; }
            = new List<DatabaseTestTheme>();

        public virtual List<DatabaseQuestion> Questions { get; set; }
            = new List<DatabaseQuestion>();
    }
}