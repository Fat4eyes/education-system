using System.Linq;
using System.Collections.Generic;
using EducationSystem.Database.Models.Source.Base;
using EducationSystem.Database.Source.Contexts;
using EducationSystem.Repositories.Interfaces.Source;

namespace EducationSystem.Repositories.Implementations.Source
{
    public abstract class RepositoryReadOnly<TModel, TOptions> : IRepositoryReadOnly<TModel>
        where TModel : DatabaseModel
    {
        protected DatabaseContext Context { get; }

        protected RepositoryReadOnly(DatabaseContext context)
        {
            Context = context;
        }

        public IQueryable<TModel> AsQueryable() => Context.Set<TModel>();

        public IEnumerable<TModel> GetAll()
        {
            return Context
                .Set<TModel>()
                .AsEnumerable();
        }

        public TModel GetById(int id)
        {
            return Context
                .Set<TModel>()
                .FirstOrDefault(x => x.Id == id);
        }

        protected virtual IQueryable<TModel> IncludeByOptions(IQueryable<TModel> query, TOptions options)
        {
            return query;
        }

        protected virtual IQueryable<TModel> FilterByOptions(IQueryable<TModel> query, TOptions options)
        {
            return query;
        }
    }
}