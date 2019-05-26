using EducationSystem.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace EducationSystem.Database.Contexts
{
    public sealed class DatabaseContext : DbContext
    {
        public DbSet<DatabaseUser> Users { get; set; }
        public DbSet<DatabaseRole> Roles { get; set; }
        public DbSet<DatabaseTest> Tests { get; set; }
        public DbSet<DatabaseFile> Files { get; set; }
        public DbSet<DatabaseGroup> Groups { get; set; }
        public DbSet<DatabaseTheme> Themes { get; set; }
        public DbSet<DatabaseAnswer> Answers { get; set; }
        public DbSet<DatabaseProgram> Programs { get; set; }
        public DbSet<DatabaseUserRole> UserRoles { get; set; }
        public DbSet<DatabaseQuestion> Questions { get; set; }
        public DbSet<DatabaseMaterial> Materials { get; set; }
        public DbSet<DatabaseStudyPlan> StudyPlans { get; set; }
        public DbSet<DatabaseInstitute> Institutes { get; set; }
        public DbSet<DatabaseTestTheme> TestThemes { get; set; }
        public DbSet<DatabaseDiscipline> Disciplines { get; set; }
        public DbSet<DatabaseProgramData> ProgramDatas { get; set; }
        public DbSet<DatabaseStudentGroup> StudentGroups { get; set; }
        public DbSet<DatabaseStudyProfile> StudyProfiles { get; set; }
        public DbSet<DatabaseProgramData> ParametersSets { get; set; }
        public DbSet<DatabaseMaterialFile> MaterialFiles { get; set; }
        public DbSet<DatabaseMaterialAnchor> MaterialAnchors { get; set; }
        public DbSet<DatabaseQuestionStudent> QuestionStudents { get; set; }
        public DbSet<DatabaseDisciplineLecturer> DisciplineLecturers { get; set; }
        public DbSet<DatabaseStudyProfileDiscipline> StudyProfileDisciplines { get; set; }
        public DbSet<DatabaseQuestionMaterialAnchor> QuestionMaterialAnchors { get; set; }

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
            BuildStudyPlan(builder);
            BuildStudyProfile(builder);
            BuildQuestion(builder);
            BuildTheme(builder);
            BuildProgram(builder);
            BuildDiscipline(builder);
            BuildMaterial(builder);

            builder
                .Entity<DatabaseAnswer>()
                .Property(x => x.IsRight)
                .HasConversion<int>();

            builder
                .Entity<DatabaseTest>()
                .Property(x => x.IsActive)
                .HasConversion<int>();

            builder
                .Entity<DatabaseQuestionStudent>()
                .Property(x => x.Passed)
                .HasConversion<int>();
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
                .HasOne(x => x.Program)
                .WithOne(x => x.Question)
                .HasForeignKey<DatabaseProgram>(x => x.QuestionId);

            builder
                .Entity<DatabaseQuestion>()
                .HasMany(x => x.QuestionStudents)
                .WithOne(x => x.Question)
                .HasForeignKey(x => x.QuestionId);

            builder
                .Entity<DatabaseQuestion>()
                .HasMany(x => x.MaterialAnchors)
                .WithOne(x => x.Question)
                .HasForeignKey(x => x.QuestionId);
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

            builder
                .Entity<DatabaseDiscipline>()
                .HasMany(x => x.Lecturers)
                .WithOne(x => x.Discipline)
                .HasForeignKey(x => x.DisciplineId);
        }

        private static void BuildProgram(ModelBuilder builder)
        {
            builder
                .Entity<DatabaseProgram>()
                .HasMany(x => x.ProgramDatas)
                .WithOne(x => x.Program)
                .HasForeignKey(x => x.ProgramId);
        }

        private static void BuildMaterial(ModelBuilder builder)
        {
            builder
                .Entity<DatabaseMaterial>()
                .HasMany(x => x.Files)
                .WithOne(x => x.Material)
                .HasForeignKey(x => x.MaterialId);

            builder
                .Entity<DatabaseMaterial>()
                .HasMany(x => x.Anchors)
                .WithOne(x => x.Material)
                .HasForeignKey(x => x.MaterialId);
        }
    }
}