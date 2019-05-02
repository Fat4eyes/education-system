using EducationSystem.Database.Contexts;
using EducationSystem.Database.Models;
using EducationSystem.Implementations.Repositories.Basics;
using EducationSystem.Interfaces.Repositories;

namespace EducationSystem.Implementations.Repositories
{
    public sealed class RepositoryProgram : Repository<DatabaseProgram>, IRepositoryProgram
    {
        public RepositoryProgram(DatabaseContext context) : base(context) { }
    }
}