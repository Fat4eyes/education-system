using System.Collections.Generic;
using System.Threading.Tasks;
using EducationSystem.Database.Contexts;
using EducationSystem.Database.Models.Basics;
using EducationSystem.Interfaces.Repositories;

namespace EducationSystem.Implementations.Repositories
{
    public class Repository<TEntity> : RepositoryReadOnly<TEntity>, IRepository<TEntity> where TEntity : DatabaseModel
    {
        public Repository(DatabaseContext context) : base(context) { }

        public async Task<TEntity> AddAsync(TEntity entity, bool save = false)
        {
            await Context
                .Set<TEntity>()
                .AddAsync(entity);

            if (save)
                await SaveChangesAsync();

            return entity;
        }

        public async Task AddAsync(IEnumerable<TEntity> entities, bool save = false)
        {
            await Context
                .Set<TEntity>()
                .AddRangeAsync(entities);

            if (save)
                await SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity, bool save = false)
        {
            Context
                .Set<TEntity>()
                .Update(entity);

            if (save)
                await SaveChangesAsync();
        }

        public async Task UpdateAsync(IEnumerable<TEntity> entities, bool save = false)
        {
            Context
                .Set<TEntity>()
                .UpdateRange(entities);

            if (save)
                await SaveChangesAsync();
        }

        public async Task RemoveAsync(TEntity entity, bool save = false)
        {
            var existing = await Context
                .Set<TEntity>()
                .FindAsync(entity.Id);

            if (existing != null)
            {
                Context
                    .Set<TEntity>()
                    .Remove(existing);

                if (save)
                    await SaveChangesAsync();
            }
        }

        public async Task RemoveAsync(IEnumerable<TEntity> entities, bool save = false)
        {
            Context
                .Set<TEntity>()
                .RemoveRange(entities);

            if (save)
                await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}