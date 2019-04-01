using EducationSystem.Database.Contexts;
using EducationSystem.Database.Models;
using EducationSystem.Repositories.Implementations.Basics;
using EducationSystem.Repositories.Interfaces;

namespace EducationSystem.Repositories.Implementations
{
    public sealed class RepositoryAnswer : Repository<DatabaseAnswer>, IRepositoryAnswer
    {
        public RepositoryAnswer(DatabaseContext context)
            : base(context) { }
    }
}