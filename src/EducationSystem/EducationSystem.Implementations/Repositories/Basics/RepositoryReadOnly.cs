using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationSystem.Database.Contexts;
using EducationSystem.Database.Models.Basics;
using EducationSystem.Interfaces.Repositories.Basics;
using Microsoft.EntityFrameworkCore;

namespace EducationSystem.Implementations.Repositories.Basics
{
    public abstract class RepositoryReadOnly<TModel> : IRepositoryReadOnly<TModel> where TModel : DatabaseModel
    {
        protected DatabaseContext Context { get; }

        protected RepositoryReadOnly(DatabaseContext context)
        {
            Context = context;
        }

        protected IQueryable<TModel> AsQueryable()
        {
            return Context.Set<TModel>();
        }

        public Task<TModel> GetByIdAsync(int id)
        {
            return AsQueryable().FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<TModel>> GetByIdsAsync(int[] ids)
        {
            return AsQueryable()
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();
        }
    }
}