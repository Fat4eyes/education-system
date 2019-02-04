using System;
using System.Linq;
using EducationSystem.Database.Source;
using EducationSystem.Database.Models.Source;
using EducationSystem.Repositories.Interfaces.Source.Rest;

namespace EducationSystem.Repositories.Implementations.Source.Rest
{
    public class RepositoryUser : RepositoryReadOnly<DatabaseUser>, IRepositoryUser
    {
        public RepositoryUser(EducationSystemDatabaseContext context)
            : base(context) { }

        public DatabaseUser GetByEmail(string email)
        {
            return AsQueryable()
                .FirstOrDefault(x => string.Compare(x.Email, email,
                    StringComparison.InvariantCultureIgnoreCase) == 0);
        }
    }
}