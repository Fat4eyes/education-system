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

        public EducationSystemDatabaseContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}