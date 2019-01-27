using EducationSystem.Database.Models.Source;
using EducationSystem.Database.Source;
using EducationSystem.Repositories.Interfaces.Source.Rest;

namespace EducationSystem.Repositories.Implementations.Source.Rest
{
    /// <summary>
    /// Репозиторий для модели <see cref="DatabaseInstitute" />.
    /// </summary>
    public class RepositoryInstitute : RepositoryReadOnly<DatabaseInstitute>, IRepositoryInstitute
    {
        public RepositoryInstitute(EducationSystemDatabaseContext context)
            : base(context) { }
    }
}