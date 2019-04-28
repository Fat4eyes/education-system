using EducationSystem.Database.Contexts;
using EducationSystem.Database.Models;
using EducationSystem.Repositories.Implementations.Basics;
using EducationSystem.Repositories.Interfaces;

namespace EducationSystem.Repositories.Implementations
{
    public sealed class RepositoryGroup : RepositoryReadOnly<DatabaseGroup>, IRepositoryGroup
    {
        public RepositoryGroup(DatabaseContext context)
            : base(context) { }
    }
}