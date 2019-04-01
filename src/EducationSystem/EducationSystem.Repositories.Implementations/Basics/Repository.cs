using System.Collections.Generic;
using System.Threading.Tasks;
using EducationSystem.Database.Contexts;
using EducationSystem.Database.Models.Basics;
using EducationSystem.Extensions;
using EducationSystem.Repositories.Interfaces.Basics;

namespace EducationSystem.Repositories.Implementations.Basics
{
    public class Repository<TModel> : RepositoryReadOnly<TModel>, IRepository<TModel>
        where TModel : DatabaseModel
    {
        public Repository(DatabaseContext context)
            : base(context) { }

        public TModel Add(TModel model, bool save = false)
        {
            Context
                .Set<TModel>()
                .Add(model);

            if (save)
                SaveChanges();

            return model;
        }

        public async Task<TModel> AddAsync(TModel model, bool save = false)
        {
            await Context
                .Set<TModel>()
                .AddAsync(model);

            if (save)
                await SaveChangesAsync();

            return model;
        }

        public void Add(IEnumerable<TModel> models, bool save = false)
        {
            Context
                .Set<TModel>()
                .AddRange(models);

            if (save)
                SaveChanges();
        }

        public async Task AddAsync(IEnumerable<TModel> models, bool save = false)
        {
            await Context
                .Set<TModel>()
                .AddRangeAsync(models);

            if (save)
                await SaveChangesAsync();
        }

        public void Update(TModel model, bool save = false) =>
            UpdateAsync(model, save).WaitTask();

        public async Task UpdateAsync(TModel model, bool save = false)
        {
            Context
                .Set<TModel>()
                .Update(model);

            if (save)
                await SaveChangesAsync();
        }

        public void Update(IEnumerable<TModel> models, bool save = false) =>
            UpdateAsync(models, save).WaitTask();

        public async Task UpdateAsync(IEnumerable<TModel> models, bool save = false)
        {
            Context
                .Set<TModel>()
                .UpdateRange(models);

            if (save)
                await SaveChangesAsync();
        }

        public void Remove(int id, bool save = false) =>
            RemoveAsync(id, save).WaitTask();

        public async Task RemoveAsync(int id, bool save = false)
        {
            var model = GetById(id);

            if (model != null)
            {
                Context
                    .Set<TModel>()
                    .Remove(model);

                if (save)
                    await SaveChangesAsync();
            }
        }

        public void Remove(TModel model, bool save = false) =>
            RemoveAsync(model, save).WaitTask();

        public async Task RemoveAsync(TModel model, bool save = false)
        {
            var existing = Context
                .Set<TModel>()
                .Find(model.Id);

            if (existing != null)
            {
                Context
                    .Set<TModel>()
                    .Remove(existing);

                if (save)
                    await SaveChangesAsync();
            }
        }

        public void Remove(IEnumerable<TModel> models, bool save = false) =>
            RemoveAsync(models, save).WaitTask();

        public async Task RemoveAsync(IEnumerable<TModel> models, bool save = false)
        {
            Context
                .Set<TModel>()
                .RemoveRange(models);

            if (save)
                await SaveChangesAsync();
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