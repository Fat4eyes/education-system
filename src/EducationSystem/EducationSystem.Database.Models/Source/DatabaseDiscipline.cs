using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using EducationSystem.Database.Models.Source.Base;

namespace EducationSystem.Database.Models.Source
{
    [Table("discipline")]
    public class DatabaseDiscipline : DatabaseModel
    {
        [Column("name")]
        public virtual string Name { get; set; }

        [Column("abbreviation")]
        public virtual string Abbreviation { get; set; }

        [Column("description")]
        public virtual string Description { get; set; }

        public virtual List<DatabaseTest> Tests { get; set; }

        public virtual List<DatabaseTheme> Themes { get; set; }
    }
}