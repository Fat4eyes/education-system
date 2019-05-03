using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationSystem.Database.Contexts;
using EducationSystem.Database.Models.Basics;
using EducationSystem.Extensions;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Interfaces.Specifications;
using EducationSystem.Models.Filters;
using Microsoft.EntityFrameworkCore;

namespace EducationSystem.Implementations.Repositories
{
    public abstract class RepositoryReadOnly<TModel> : IRepositoryReadOnly<TModel> where TModel : DatabaseModel
    {
        protected DatabaseContext Context { get; }

        protected RepositoryReadOnly(DatabaseContext context)
        {
            Context = context;
        }

        public Task<List<TModel>> FindAllAsync(ISpecification<TModel> specification)
        {
            return Context
                .Set<TModel>()
                .Where(specification.ToExpression())
                .ToListAsync();
        }

        public Task<(int Count, List<TModel> Items)> FindPaginatedAsync(ISpecification<TModel> specification, Filter filter)
        {
            return Context
                .Set<TModel>()
                .Where(specification.ToExpression())
                .ApplyPagingAsync(filter);
        }

        public Task<TModel> FindFirstAsync(ISpecification<TModel> specification)
        {
            return Context
                .Set<TModel>()
                .FirstOrDefaultAsync(specification.ToExpression());
        }
    }
}