using System.Linq;
using System.Collections.Generic;
using EducationSystem.Database.Models.Source;
using EducationSystem.Database.Source;
using EducationSystem.Repositories.Interfaces.Source;

namespace EducationSystem.Repositories.Implementations.Source
{
    /// <summary>
    /// Репозиторий (только для получения данных).
    /// </summary>
    public class RepositoryReadOnly<TModel> : IRepositoryReadOnly<TModel>
        where TModel : DatabaseModel
    {
        protected EducationSystemDatabaseContext Context { get; }

        public RepositoryReadOnly(EducationSystemDatabaseContext context)
        {
            Context = context;
        }

        /// <inheritdoc />
        public IQueryable<TModel> AsQueryable() => Context.Set<TModel>();

        /// <inheritdoc />
        public IEnumerable<TModel> GetAll()
        {
            return Context
                .Set<TModel>()
                .AsEnumerable();
        }

        /// <inheritdoc />
        public TModel GetById(int id)
        {
            return Context
                .Set<TModel>()
                .FirstOrDefault(x => x.Id == id);
        }
    }
}