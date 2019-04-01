using EducationSystem.Database.Contexts;
using EducationSystem.Database.Models;
using EducationSystem.Repositories.Implementations.Basics;
using EducationSystem.Repositories.Interfaces;

namespace EducationSystem.Repositories.Implementations
{
    public sealed class RepositoryTestTheme : Repository<DatabaseTestTheme>, IRepositoryTestTheme
    {
        public RepositoryTestTheme(DatabaseContext context)
            : base(context) { }
    }
}