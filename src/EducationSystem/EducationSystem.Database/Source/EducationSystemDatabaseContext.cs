using EducationSystem.Database.Models.Source;
using Microsoft.EntityFrameworkCore;

namespace EducationSystem.Database.Source
{
    public sealed class EducationSystemDatabaseContext : DbContext
    {
        public DbSet<DatabaseUser> Users { get; set; }

        public EducationSystemDatabaseContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}