using System;
using System.Linq;
using EducationSystem.Database.Source;
using EducationSystem.Database.Models.Source;
using EducationSystem.Repositories.Interfaces.Source.Rest;

namespace EducationSystem.Repositories.Implementations.Source.Rest
{
    public class UserRepository : ReadOnlyRepository<DatabaseUser>, IUserRepository
    {
        public UserRepository(EducationSystemDatabaseContext context)
            : base(context) { }

        public DatabaseUser GetByEmail(string email)
        {
            return AsQueryable()
                .FirstOrDefault(x => string.Equals(x.Email, email,
                    StringComparison.CurrentCultureIgnoreCase));
        }
    }
}