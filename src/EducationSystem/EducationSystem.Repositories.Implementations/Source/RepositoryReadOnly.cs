using System.Linq;
using EducationSystem.Database.Models.Source.Base;
using EducationSystem.Database.Source.Contexts;
using EducationSystem.Repositories.Interfaces.Source;

namespace EducationSystem.Repositories.Implementations.Source
{
    public abstract class RepositoryReadOnly<TModel> : IRepositoryReadOnly<TModel>
        where TModel : DatabaseModel
    {
        protected DatabaseContext Context { get; }

        protected RepositoryReadOnly(DatabaseContext context)
        {
            Context = context;
        }

        protected IQueryable<TModel> AsQueryable()  => Context.Set<TModel>();

        public TModel GetById(int id) => AsQueryable()
            .FirstOrDefault(x => x.Id == id);
    }
}