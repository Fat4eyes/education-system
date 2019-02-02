using EducationSystem.Database.Models.Source;
using EducationSystem.Database.Source;
using EducationSystem.Repositories.Interfaces.Source.Rest;

namespace EducationSystem.Repositories.Implementations.Source.Rest
{
    /// <summary>
    /// Репозиторий для модели <see cref="DatabaseGroup" />.
    /// </summary>
    public class GroupRepository : ReadOnlyRepository<DatabaseGroup>, IGroupRepository
    {
        public GroupRepository(EducationSystemDatabaseContext context)
            : base(context) { }
    }
}