using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using EducationSystem.Database.Models.Source.Base;

namespace EducationSystem.Database.Models.Source
{
    [Table("theme")]
    public class DatabaseTheme : DatabaseModel
    {
        [Column("name")]
        public virtual string Name { get; set; }

        [Column("discipline_id")]
        public virtual int DisciplineId { get; set; }

        [ForeignKey(nameof(DisciplineId))]
        public virtual DatabaseDiscipline Discipline { get; set; }

        public virtual List<DatabaseTestTheme> ThemeTests { get; set; }

        public virtual List<DatabaseQuestion> Questions { get; set; }
    }
}