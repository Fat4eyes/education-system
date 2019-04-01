using EducationSystem.Database.Models;
using EducationSystem.Database.Source.Contexts;
using EducationSystem.Repositories.Implementations.Basics;
using EducationSystem.Repositories.Interfaces;

namespace EducationSystem.Repositories.Implementations
{
    public sealed class RepositoryProgram : Repository<DatabaseProgram>, IRepositoryProgram
    {
        public RepositoryProgram(DatabaseContext context)
            : base(context) { }
    }
}