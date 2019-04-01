using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using EducationSystem.Database.Models.Basics;

namespace EducationSystem.Database.Models
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

        public virtual List<DatabaseUserRole> UserRoles { get; set; }
            = new List<DatabaseUserRole>();

        public virtual DatabaseStudentGroup StudentGroup { get; set; }

        public virtual List<DatabaseTestResult> TestResults { get; set; }
            = new List<DatabaseTestResult>();
    }
}