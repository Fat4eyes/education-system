using System.Collections.Generic;
using System.Threading.Tasks;
using EducationSystem.Database.Contexts;
using EducationSystem.Database.Models.Basics;
using EducationSystem.Interfaces.Repositories;

namespace EducationSystem.Implementations.Repositories
{
    public class Repository<TModel> : RepositoryReadOnly<TModel>, IRepository<TModel> where TModel : DatabaseModel
    {
        public Repository(DatabaseContext context) : base(context) { }

        public async Task<TModel> AddAsync(TModel model, bool save = false)
        {
            await Context
                .Set<TModel>()
                .AddAsync(model);

            if (save)
                await SaveChangesAsync();

            return model;
        }

        public async Task AddAsync(IEnumerable<TModel> models, bool save = false)
        {
            await Context
                .Set<TModel>()
                .AddRangeAsync(models);

            if (save)
                await SaveChangesAsync();
        }

        public async Task UpdateAsync(TModel model, bool save = false)
        {
            Context
                .Set<TModel>()
                .Update(model);

            if (save)
                await SaveChangesAsync();
        }

        public async Task UpdateAsync(IEnumerable<TModel> models, bool save = false)
        {
            Context
                .Set<TModel>()
                .UpdateRange(models);

            if (save)
                await SaveChangesAsync();
        }

        public async Task RemoveAsync(TModel model, bool save = false)
        {
            var existing = await Context
                .Set<TModel>()
                .FindAsync(model.Id);

            if (existing != null)
            {
                Context
                    .Set<TModel>()
                    .Remove(existing);

                if (save)
                    await SaveChangesAsync();
            }
        }

        public async Task RemoveAsync(IEnumerable<TModel> models, bool save = false)
        {
            Context
                .Set<TModel>()
                .RemoveRange(models);

            if (save)
                await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}