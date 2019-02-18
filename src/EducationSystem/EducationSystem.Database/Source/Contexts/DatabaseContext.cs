using EducationSystem.Database.Models.Source;
using Microsoft.EntityFrameworkCore;

namespace EducationSystem.Database.Source.Contexts
{
    public sealed class DatabaseContext : DbContext
    {
        public DbSet<DatabaseUser> Users { get; set; }
        public DbSet<DatabaseRole> Roles { get; set; }
        public DbSet<DatabaseTest> Tests { get; set; }
        public DbSet<DatabaseGroup> Groups { get; set; }
        public DbSet<DatabaseAnswer> Answers { get; set; }
        public DbSet<DatabaseProgram> Programs { get; set; }
        public DbSet<DatabaseQuestion> Questions { get; set; }
        public DbSet<DatabaseStudyPlan> StudyPlans { get; set; }
        public DbSet<DatabaseInstitute> Institutes { get; set; }
        public DbSet<DatabaseDiscipline> Disciplines { get; set; }
        public DbSet<DatabaseTestResult> TestResults { get; set; }
        public DbSet<DatabaseGivenAnswer> GivenAnswers { get; set; }
        public DbSet<DatabaseStudyProfile> StudyProfiles { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            BuildUserRole(builder);
            BuildStudentGroup(builder);
            BuildTestTheme(builder);
            BuildStudyProfileDiscipline(builder);

            BuildTestResult(builder);
            BuildStudyPlan(builder);
            BuildStudyProfile(builder);
            BuildQuestion(builder);
            BuildTheme(builder);
            BuildUser(builder);
            BuildDiscipline(builder);
        }

        private static void BuildUserRole(ModelBuilder builder)
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
                .Entity<DatabaseUserRole>()
                .HasOne(x => x.Role)
                .WithMany(x => x.RoleUsers)
                .HasForeignKey(x => x.RoleId);
        }

        private static void BuildStudentGroup(ModelBuilder builder)
        {
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
        }

        private static void BuildTestTheme(ModelBuilder builder)
        {
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

        public static void BuildStudyProfileDiscipline(ModelBuilder builder)
        {
            builder
                .Entity<DatabaseStudyProfileDiscipline>()
                .HasKey(x => new { x.DisciplineId, x.StudyProfileId });

            builder
                .Entity<DatabaseStudyProfileDiscipline>()
                .HasOne(x => x.Discipline)
                .WithMany(x => x.StudyProfiles)
                .HasForeignKey(x => x.DisciplineId);

            builder
                .Entity<DatabaseStudyProfileDiscipline>()
                .HasOne(x => x.StudyProfile)
                .WithMany(x => x.Disciplines)
                .HasForeignKey(x => x.StudyProfileId);
        }

        private static void BuildTestResult(ModelBuilder builder)
        {
            builder
                .Entity<DatabaseTestResult>()
                .HasMany(x => x.GivenAnswers)
                .WithOne(x => x.TestResult)
                .HasForeignKey(x => x.TestResultId);
        }

        private static void BuildStudyPlan(ModelBuilder builder)
        {
            builder
                .Entity<DatabaseStudyPlan>()
                .HasMany(x => x.Groups)
                .WithOne(x => x.StudyPlan)
                .HasForeignKey(x => x.StudyPlanId);
        }

        private static void BuildStudyProfile(ModelBuilder builder)
        {
            builder
                .Entity<DatabaseStudyProfile>()
                .HasMany(x => x.StudyPlans)
                .WithOne(x => x.StudyProfile)
                .HasForeignKey(x => x.StudyProfileId);
        }

        private static void BuildQuestion(ModelBuilder builder)
        {
            builder
                .Entity<DatabaseQuestion>()
                .HasMany(x => x.Answers)
                .WithOne(x => x.Question)
                .HasForeignKey(x => x.QuestionId);

            builder
                .Entity<DatabaseQuestion>()
                .HasMany(x => x.GivenAnswers)
                .WithOne(x => x.Question)
                .HasForeignKey(x => x.QuestionId);

            builder
                .Entity<DatabaseQuestion>()
                .HasOne(x => x.Program)
                .WithOne(x => x.Question)
                .HasForeignKey<DatabaseProgram>(x => x.QuestionId);
        }

        private static void BuildTheme(ModelBuilder builder)
        {
            builder
                .Entity<DatabaseTheme>()
                .HasMany(x => x.Questions)
                .WithOne(x => x.Theme)
                .HasForeignKey(x => x.ThemeId);
        }

        private static void BuildDiscipline(ModelBuilder builder)
        {
            builder
                .Entity<DatabaseDiscipline>()
                .HasMany(x => x.Tests)
                .WithOne(x => x.Discipline)
                .HasForeignKey(x => x.DisciplineId);

            builder
                .Entity<DatabaseDiscipline>()
                .HasMany(x => x.Themes)
                .WithOne(x => x.Discipline)
                .HasForeignKey(x => x.DisciplineId);
        }

        private static void BuildUser(ModelBuilder builder)
        {
            builder
                .Entity<DatabaseUser>()
                .HasMany(x => x.TestResults)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
        }
    }
}