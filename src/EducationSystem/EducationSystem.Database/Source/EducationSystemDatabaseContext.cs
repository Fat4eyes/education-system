using EducationSystem.Database.Models.Source;
using Microsoft.EntityFrameworkCore;

namespace EducationSystem.Database.Source
{
    public sealed class EducationSystemDatabaseContext : DbContext
    {
        /// <summary>
        /// Пользователи.
        /// </summary>
        public DbSet<DatabaseUser> Users { get; set; }

        /// <summary>
        /// Группы.
        /// </summary>
        public DbSet<DatabaseGroup> Groups { get; set; }

        public EducationSystemDatabaseContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}