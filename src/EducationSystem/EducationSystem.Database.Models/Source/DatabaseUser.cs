using System.ComponentModel.DataAnnotations.Schema;

namespace EducationSystem.Database.Models.Source
{
    [Table("user")]
    public class DatabaseUser : DatabaseModel
    {
        [Column("firstname")]
        public virtual string FirstName { get; set; }

        [Column("patronymic")]
        public virtual string MiddleName { get; set; }

        [Column("lastname")]
        public virtual string LastName { get; set; }

        [Column("email")]
        public virtual string Email { get; set; }

        [Column("password")]
        public virtual string Password { get; set; }

        [Column("active")]
        public virtual int Active { get; set; }
    }
}