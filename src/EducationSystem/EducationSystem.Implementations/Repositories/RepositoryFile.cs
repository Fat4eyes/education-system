using System;
using System.Threading.Tasks;
using EducationSystem.Database.Contexts;
using EducationSystem.Database.Models;
using EducationSystem.Implementations.Repositories.Basics;
using EducationSystem.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EducationSystem.Implementations.Repositories
{
    public class RepositoryFile : Repository<DatabaseFile>, IRepositoryFile
    {
        public RepositoryFile(DatabaseContext context) : base(context) { }

        public Task<DatabaseFile> GetFileAsync(Guid guid)
        {
            return AsQueryable()
                .FirstOrDefaultAsync(x => string.Equals(
                    x.Guid, guid.ToString(),
                    StringComparison.InvariantCultureIgnoreCase));
        }
    }
}