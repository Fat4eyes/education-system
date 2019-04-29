using System;
using System.Threading.Tasks;
using EducationSystem.Database.Contexts;
using EducationSystem.Database.Models;
using EducationSystem.Repositories.Implementations.Basics;
using EducationSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EducationSystem.Repositories.Implementations
{
    public class RepositoryFile : Repository<DatabaseFile>, IRepositoryFile
    {
        public RepositoryFile(DatabaseContext context)
            : base(context) { }

        public Task<DatabaseFile> GetByGuid(Guid guid)
        {
            return AsQueryable().FirstOrDefaultAsync(x => string.Equals(
                x.Guid, guid.ToString(), StringComparison.InvariantCultureIgnoreCase));
        }

        public Task<bool> IsFileExists(int id)
        {
            return AsQueryable().AnyAsync(x => x.Id == id);
        }
    }
}