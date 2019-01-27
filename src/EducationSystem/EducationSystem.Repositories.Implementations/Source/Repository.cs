using System.Linq;
using System.Threading.Tasks;
using EducationSystem.Database.Models.Source;
using EducationSystem.Database.Source;
using EducationSystem.Repositories.Interfaces.Source;
using Microsoft.EntityFrameworkCore;

namespace EducationSystem.Repositories.Implementations.Source
{
    /// <summary>
    /// Репозиторий.
    /// </summary>
    public class Repository<TModel> : RepositoryReadOnly<TModel>, IRepository<TModel>
        where TModel : DatabaseModel
    {
        public Repository(EducationSystemDatabaseContext context)
            : base(context) { }

        /// <inheritdoc />
        public TModel Add(TModel model)
        {
            Context
                .Set<TModel>()
                .Add(model);

            return model;
        }

        /// <inheritdoc />
        public async Task<TModel> AddAsync(TModel model)
        {
            await Context
                .Set<TModel>()
                .AddAsync(model);

            return model;
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        /// <inheritdoc />
        public async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}