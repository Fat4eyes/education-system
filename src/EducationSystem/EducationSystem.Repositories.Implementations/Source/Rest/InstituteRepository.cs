using EducationSystem.Database.Models.Source;
using EducationSystem.Database.Source;
using EducationSystem.Repositories.Interfaces.Source.Rest;

namespace EducationSystem.Repositories.Implementations.Source.Rest
{
    /// <summary>
    /// Репозиторий для модели <see cref="DatabaseInstitute" />.
    /// </summary>
    public class InstituteRepository : ReadOnlyRepository<DatabaseInstitute>, IInstituteRepository
    {
        public InstituteRepository(EducationSystemDatabaseContext context)
            : base(context) { }
    }
}