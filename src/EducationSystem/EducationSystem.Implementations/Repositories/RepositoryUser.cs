using System;
using System.Threading.Tasks;
using EducationSystem.Database.Contexts;
using EducationSystem.Database.Models;
using EducationSystem.Implementations.Repositories.Basics;
using EducationSystem.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EducationSystem.Implementations.Repositories
{
    public sealed class RepositoryUser : RepositoryReadOnly<DatabaseUser>, IRepositoryUser
    {
        public RepositoryUser(DatabaseContext context) : base(context) { }

        public Task<DatabaseUser> GetUserByEmailAsync(string email)
        {
            return AsQueryable()
                .FirstOrDefaultAsync(x => string.Equals(
                    x.Email, email,
                    StringComparison.InvariantCultureIgnoreCase));
        }
    }
}