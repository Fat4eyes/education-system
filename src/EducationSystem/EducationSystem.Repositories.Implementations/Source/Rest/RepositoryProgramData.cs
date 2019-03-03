using EducationSystem.Database.Models.Source;
using EducationSystem.Database.Source.Contexts;
using EducationSystem.Repositories.Interfaces.Source.Rest;

namespace EducationSystem.Repositories.Implementations.Source.Rest
{
    public sealed class RepositoryProgramData : Repository<DatabaseProgramData>, IRepositoryProgramData
    {
        public RepositoryProgramData(DatabaseContext context)
            : base(context) { }
    }
}