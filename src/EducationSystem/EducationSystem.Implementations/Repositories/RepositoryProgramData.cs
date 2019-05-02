using EducationSystem.Database.Contexts;
using EducationSystem.Database.Models;
using EducationSystem.Implementations.Repositories.Basics;
using EducationSystem.Interfaces.Repositories;

namespace EducationSystem.Implementations.Repositories
{
    public sealed class RepositoryProgramData : Repository<DatabaseProgramData>, IRepositoryProgramData
    {
        public RepositoryProgramData(DatabaseContext context) : base(context) { }
    }
}