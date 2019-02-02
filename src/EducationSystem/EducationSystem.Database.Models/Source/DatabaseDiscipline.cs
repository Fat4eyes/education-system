using System.ComponentModel.DataAnnotations.Schema;
using EducationSystem.Database.Models.Source.Base;

namespace EducationSystem.Database.Models.Source
{
    [Table("discipline")]
    public class DatabaseDiscipline : DatabaseModel
    {
        [Column("name")]
        public string Name { get; set; }

        [Column("abbreviation")]
        public string Abbreviation { get; set; }

        [Column("description")]
        public string Description { get; set; }
    }
}