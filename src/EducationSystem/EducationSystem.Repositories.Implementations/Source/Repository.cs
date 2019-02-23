using System.Linq;
using System.Threading.Tasks;
using EducationSystem.Database.Models.Source.Base;
using EducationSystem.Database.Source.Contexts;
using EducationSystem.Repositories.Interfaces.Source;
using Microsoft.EntityFrameworkCore;

namespace EducationSystem.Repositories.Implementations.Source
{
    public class Repository<TModel> : RepositoryReadOnly<TModel>, IRepository<TModel>
        where TModel : DatabaseModel
    {
        public Repository(DatabaseContext context)
            : base(context) { }

        public TModel Add(TModel model)
        {
            Context
                .Set<TModel>()
                .Add(model);

            return model;
        }

        public async Task<TModel> AddAsync(TModel model)
        {
            await Context
                .Set<TModel>()
                .AddAsync(model);

            return model;
        }

        public TModel Update(TModel model)
        {
            Context
                .Entry(model)
                .State = EntityState.Modified;

            Context
                .Set<TModel>()
                .Attach(model);

            return model;
        }

        public void Delete(int id)
        {
            var model = AsQueryable()
                .FirstOrDefault(x => x.Id == id);

            if (model != null)
            {
                Context
                    .Set<TModel>()
                    .Remove(model);
            }
        }

        public void Delete(TModel model)
        {
            var existing = Context
                .Set<TModel>()
                .Find(model);

            if (existing != null)
            {
                Context
                    .Set<TModel>()
                    .Remove(existing);
            }
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}