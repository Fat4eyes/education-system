using EducationSystem.Database.Models.Source;
using Microsoft.EntityFrameworkCore;

namespace EducationSystem.Database.Source
{
    public sealed class EducationSystemDatabaseContext : DbContext
    {
        public DbSet<DatabaseUser> Users { get; set; }
        public DbSet<DatabaseGroup> Groups { get; set; }
        public DbSet<DatabaseStudyPlan> StudyPlans { get; set; }
        public DbSet<DatabaseStudyProfile> StudyProfiles { get; set; }
        public DbSet<DatabaseInstitute> Institutes { get; set; }
        public DbSet<DatabaseRole> Roles { get; set; }
        public DbSet<DatabaseDiscipline> Disciplines { get; set; }
        public DbSet<DatabaseUserRole> UserRoles { get; set; }

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