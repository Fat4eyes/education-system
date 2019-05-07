using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationSystem.Database.Contexts;
using EducationSystem.Database.Models.Basics;
using EducationSystem.Extensions;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Models.Filters;
using EducationSystem.Specifications;
using Microsoft.EntityFrameworkCore;

namespace EducationSystem.Implementations.Repositories
{
    public abstract class RepositoryReadOnly<TEntity> : IRepositoryReadOnly<TEntity> where TEntity : DatabaseModel
    {
        protected DatabaseContext Context { get; }

        protected RepositoryReadOnly(DatabaseContext context)
        {
            Context = context;
        }

        protected IQueryable<TEntity> Entities => Context.Set<TEntity>();

        public virtual Task<List<TEntity>> FindAllAsync(ISpecification<TEntity> specification)
        {
            return Entities
                .Where(specification.ToExpression())
                .ToListAsync();
        }

        public virtual Task<(int Count, List<TEntity> Items)> FindPaginatedAsync(ISpecification<TEntity> specification, Filter filter)
        {
            return Entities
                .Where(specification.ToExpression())
                .ApplyPagingAsync(filter);
        }

        public virtual Task<TEntity> FindFirstAsync(ISpecification<TEntity> specification)
        {
            return Entities.FirstOrDefaultAsync(specification.ToExpression());
        }
    }
}