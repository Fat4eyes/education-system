using EducationSystem.Database.Models.Source;
using EducationSystem.Database.Source.Contexts;
using EducationSystem.Repositories.Interfaces.Source.Rest;

namespace EducationSystem.Repositories.Implementations.Source.Rest
{
    public sealed class RepositoryProgram : Repository<DatabaseProgram>, IRepositoryProgram
    {
        public RepositoryProgram(DatabaseContext context)
            : base(context) { }
    }
}