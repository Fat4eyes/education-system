using System;
using System.Linq;
using EducationSystem.Database.Models.Source;
using EducationSystem.Database.Source.Contexts;
using EducationSystem.Repositories.Interfaces.Source.Rest;

namespace EducationSystem.Repositories.Implementations.Source.Rest
{
    public class RepositoryFile : Repository<DatabaseFile>, IRepositoryFile
    {
        public RepositoryFile(DatabaseContext context)
            : base(context) { }

        public DatabaseFile GetByGuid(Guid guid)
        {
            return AsQueryable().FirstOrDefault(x => x.Guid == guid);
        }
    }
}