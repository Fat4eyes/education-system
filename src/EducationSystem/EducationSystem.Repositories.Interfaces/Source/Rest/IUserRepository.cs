using EducationSystem.Database.Models.Source;

namespace EducationSystem.Repositories.Interfaces.Source.Rest
{
    public interface IUserRepository : IReadOnlyRepository<DatabaseUser>
    {
        DatabaseUser GetByEmail(string email);
    }
}