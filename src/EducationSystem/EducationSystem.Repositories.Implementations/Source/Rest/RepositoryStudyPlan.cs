using EducationSystem.Database.Models.Source;
using EducationSystem.Database.Source;
using EducationSystem.Repositories.Interfaces.Source.Rest;

namespace EducationSystem.Repositories.Implementations.Source.Rest
{
    /// <summary>
    /// Репозиторий для модели <see cref="DatabaseStudyPlan" />.
    /// </summary>
    public class RepositoryStudyPlan : RepositoryReadOnly<DatabaseStudyPlan>, IRepositoryStudyPlan
    {
        public RepositoryStudyPlan(EducationSystemDatabaseContext context)
            : base(context) { }
    }
}