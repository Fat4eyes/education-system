using EducationSystem.Database.Contexts;
using EducationSystem.Database.Models;
using EducationSystem.Implementations.Repositories.Basics;
using EducationSystem.Interfaces.Repositories;

namespace EducationSystem.Implementations.Repositories
{
    public sealed class RepositoryAnswer : Repository<DatabaseAnswer>, IRepositoryAnswer
    {
        public RepositoryAnswer(DatabaseContext context) : base(context) { }
    }
}