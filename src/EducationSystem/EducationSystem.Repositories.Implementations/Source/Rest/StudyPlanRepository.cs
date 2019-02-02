using EducationSystem.Database.Models.Source;
using EducationSystem.Database.Source;
using EducationSystem.Repositories.Interfaces.Source.Rest;

namespace EducationSystem.Repositories.Implementations.Source.Rest
{
    /// <summary>
    /// Репозиторий для модели <see cref="DatabaseStudyPlan" />.
    /// </summary>
    public class StudyPlanRepository : ReadOnlyRepository<DatabaseStudyPlan>, IStudyPlanRepository
    {
        public StudyPlanRepository(EducationSystemDatabaseContext context)
            : base(context) { }
    }
}