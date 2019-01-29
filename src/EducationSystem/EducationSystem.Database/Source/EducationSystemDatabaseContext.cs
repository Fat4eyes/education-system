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

        /// <summary>
        /// Учебные планы.
        /// </summary>
        public DbSet<DatabaseStudyPlan> StudyPlans { get; set; }

        /// <summary>
        /// Профили обучения.
        /// </summary>
        public DbSet<DatabaseStudyProfile> StudyProfiles { get; set; }

        /// <summary>
        /// Институты.
        /// </summary>
        public DbSet<DatabaseInstitute> Institutes { get; set; }

        /// <summary>
        /// Роли пользователей.
        /// </summary>
        public DbSet<DatabaseRole> UserRoles { get; set; }

        public EducationSystemDatabaseContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<DatabaseUserRole>()
                .HasKey(x => new { x.RoleId, x.UserId });

            builder
                .Entity<DatabaseUserRole>()
                .HasOne(x => x.User)
                .WithMany(s => s.Roles)
                .HasForeignKey(sc => sc.UserId);
        }
    }
}