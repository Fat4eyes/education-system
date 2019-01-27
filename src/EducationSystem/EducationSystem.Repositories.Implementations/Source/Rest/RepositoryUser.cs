using EducationSystem.Database.Source;
using EducationSystem.Database.Models.Source;
using EducationSystem.Repositories.Interfaces.Source.Rest;

namespace EducationSystem.Repositories.Implementations.Source.Rest
{
    /// <summary>
    /// Репозиторий для модели <see cref="DatabaseUser" />.
    /// </summary>
    public class RepositoryUser : RepositoryReadOnly<DatabaseUser>, IRepositoryUser
    {
        public RepositoryUser(EducationSystemDatabaseContext context) : base(context) { }
    }
}