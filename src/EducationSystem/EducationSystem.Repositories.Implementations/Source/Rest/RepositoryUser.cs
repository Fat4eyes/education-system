using System;
using System.Linq;
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
        public RepositoryUser(EducationSystemDatabaseContext context)
            : base(context) { }

        /// <inheritdoc />
        public DatabaseUser GetByEmailAndPassword(string email, string password)
        {
            return AsQueryable()
                .FirstOrDefault(x => x.Password == password
                    && string.Equals(x.Email, email, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}