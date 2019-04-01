using EducationSystem.Database.Contexts;
using EducationSystem.Database.Models;
using EducationSystem.Repositories.Implementations.Basics;
using EducationSystem.Repositories.Interfaces;

namespace EducationSystem.Repositories.Implementations
{
    public sealed class RepositoryProgramData : Repository<DatabaseProgramData>, IRepositoryProgramData
    {
        public RepositoryProgramData(DatabaseContext context)
            : base(context) { }
    }
}