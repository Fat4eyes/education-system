using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationSystem.Database.Models.Source.Base;
using EducationSystem.Database.Source.Contexts;
using EducationSystem.Repositories.Interfaces.Source;

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

        public void AddRange(IEnumerable<TModel> models)
        {
            Context
                .Set<TModel>()
                .AddRange(models);
        }

        public async Task AddRangeAsync(IEnumerable<TModel> models)
        {
            await Context
                .Set<TModel>()
                .AddRangeAsync(models);
        }

        public void Update(TModel model)
        {
            Context
                .Set<TModel>()
                .Update(model);
        }

        public void UpdateRange(IEnumerable<TModel> models)
        {
            Context
                .Set<TModel>()
                .UpdateRange(models);
        }

        public void Remove(int id)
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

        public void Remove(TModel model)
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

        public void RemoveRange(IEnumerable<TModel> models)
        {
            Context
                .Set<TModel>()
                .RemoveRange(models);
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