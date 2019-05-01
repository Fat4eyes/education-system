using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationSystem.Database.Contexts;
using EducationSystem.Database.Models;
using EducationSystem.Extensions;
using EducationSystem.Models.Filters;
using EducationSystem.Repositories.Implementations.Basics;
using EducationSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EducationSystem.Repositories.Implementations
{
    public sealed class RepositoryUser : RepositoryReadOnly<DatabaseUser>, IRepositoryUser
    {
        public RepositoryUser(DatabaseContext context)
            : base(context) { }

        public Task<DatabaseUser> GetUserByEmailAsync(string email)
        {
            return AsQueryable()
                .FirstOrDefaultAsync(x => string.Equals(
                    x.Email, email,
                    StringComparison.InvariantCultureIgnoreCase));
        }
    }
}