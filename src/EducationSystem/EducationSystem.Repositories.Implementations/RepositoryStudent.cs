using EducationSystem.Database.Contexts;
using EducationSystem.Database.Models;
using EducationSystem.Repositories.Implementations.Basics;
using EducationSystem.Repositories.Interfaces;

namespace EducationSystem.Repositories.Implementations
{
    public sealed class RepositoryStudent : RepositoryReadOnly<DatabaseUser>, IRepositoryStudent
    {
        public RepositoryStudent(DatabaseContext context)
            : base(context) { }
    }
}