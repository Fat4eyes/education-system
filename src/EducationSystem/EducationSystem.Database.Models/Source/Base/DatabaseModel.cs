using System.ComponentModel.DataAnnotations.Schema;

namespace EducationSystem.Database.Models.Source.Base
{
    public class DatabaseModel
    {
        [Column("id")]
        public virtual int Id { get; set; }
    }
}