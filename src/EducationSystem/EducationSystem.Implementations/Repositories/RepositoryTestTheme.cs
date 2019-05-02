using EducationSystem.Database.Contexts;
using EducationSystem.Database.Models;
using EducationSystem.Implementations.Repositories.Basics;
using EducationSystem.Interfaces.Repositories;

namespace EducationSystem.Implementations.Repositories
{
    public sealed class RepositoryTestTheme : Repository<DatabaseTestTheme>, IRepositoryTestTheme
    {
        public RepositoryTestTheme(DatabaseContext context) : base(context) { }
    }
}