using System.ComponentModel.DataAnnotations.Schema;
using EducationSystem.Database.Models.Basics;

namespace EducationSystem.Database.Models
{
    [Table("role_user")]
    public class DatabaseUserRole : DatabaseModel
    {
        [Column("role_id")]
        public virtual int RoleId { get; set; }

        [ForeignKey(nameof(RoleId))]
        public virtual DatabaseRole Role { get; set; }

        [Column("user_id")]
        public virtual int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual DatabaseUser User { get; set; }
    }
}