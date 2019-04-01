using EducationSystem.Database.Models;
using EducationSystem.Database.Source.Contexts;
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