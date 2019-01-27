using EducationSystem.Database.Models.Source;
using EducationSystem.Database.Source;
using EducationSystem.Repositories.Interfaces.Source.Rest;

namespace EducationSystem.Repositories.Implementations.Source.Rest
{
    /// <summary>
    /// Репозиторий для модели <see cref="DatabaseStudyProfile" />.
    /// </summary>
    public class RepositoryStudyProfile : RepositoryReadOnly<DatabaseStudyProfile>, IRepositoryStudyProfile
    {
        public RepositoryStudyProfile(EducationSystemDatabaseContext context)
            : base(context) { }
    }
}