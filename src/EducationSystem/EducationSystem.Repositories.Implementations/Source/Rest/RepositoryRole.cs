using EducationSystem.Database.Models.Source;
using EducationSystem.Database.Source;
using EducationSystem.Repositories.Interfaces.Source.Rest;

namespace EducationSystem.Repositories.Implementations.Source.Rest
{
    /// <summary>
    /// Репозиторий для модели <see cref="DatabaseRole" />.
    /// </summary>
    public class RepositoryRole : RepositoryReadOnly<DatabaseRole>, IRepositoryRole
    {
        public RepositoryRole(EducationSystemDatabaseContext context)
            : base(context) { }
    }
}