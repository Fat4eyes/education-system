using EducationSystem.Database.Contexts;
using EducationSystem.Database.Models;
using EducationSystem.Implementations.Repositories.Basics;
using EducationSystem.Interfaces.Repositories;

namespace EducationSystem.Implementations.Repositories
{
    public sealed class RepositoryGroup : RepositoryReadOnly<DatabaseGroup>, IRepositoryGroup
    {
        public RepositoryGroup(DatabaseContext context) : base(context) { }
    }
}