using EducationSystem.Database.Models.Source;
using EducationSystem.Database.Source.Contexts;
using EducationSystem.Repositories.Interfaces.Source.Rest;

namespace EducationSystem.Repositories.Implementations.Source.Rest
{
    public class RepositoryTestTheme : Repository<DatabaseTestTheme>, IRepositoryTestTheme
    {
        public RepositoryTestTheme(DatabaseContext context)
            : base(context) { }
    }
}