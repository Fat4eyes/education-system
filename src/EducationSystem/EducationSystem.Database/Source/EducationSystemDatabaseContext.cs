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
        public DbSet<DatabaseTest> Tests { get; set; }
        public DbSet<DatabaseTestResult> TestResults { get; set; }

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
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.UserId);

            builder
                .Entity<DatabaseStudentGroup>()
                .HasKey(x => new { x.StudentId, x.GroupId });

            builder
                .Entity<DatabaseStudentGroup>()
                .HasOne(x => x.Student)
                .WithOne(x => x.StudentGroup);

            builder
                .Entity<DatabaseStudentGroup>()
                .HasOne(x => x.Group)
                .WithMany(x => x.GroupStudents)
                .HasForeignKey(x => x.GroupId);

            builder
                .Entity<DatabaseTestResult>()
                .HasKey(x => new { x.UserId, x.TestId });

            builder
                .Entity<DatabaseTestResult>()
                .HasOne(x => x.User)
                .WithMany(x => x.TestResults)
                .HasForeignKey(x => x.UserId);

            builder
                .Entity<DatabaseTestTheme>()
                .HasKey(x => new { x.TestId, x.ThemeId });

            builder
                .Entity<DatabaseTestTheme>()
                .HasOne(x => x.Test)
                .WithMany(x => x.TestThemes)
                .HasForeignKey(x => x.TestId);

            builder
                .Entity<DatabaseTestTheme>()
                .HasOne(x => x.Theme)
                .WithMany(x => x.ThemeTests)
                .HasForeignKey(x => x.ThemeId);
        }
    }
}