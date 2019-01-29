using System.ComponentModel.DataAnnotations.Schema;
using EducationSystem.Database.Models.Source.Base;

namespace EducationSystem.Database.Models.Source
{
    /// <summary>
    /// Доменная модель: Роль пользователя (Роль-Пользователь).
    /// </summary>
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