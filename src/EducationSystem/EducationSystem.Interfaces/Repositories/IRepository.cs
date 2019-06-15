using System.Collections.Generic;
using System.Threading.Tasks;
using EducationSystem.Database.Models.Basics;

namespace EducationSystem.Interfaces.Repositories
{
    public interface IRepository<TEntity> : IRepositoryReadOnly<TEntity> where TEntity : DatabaseModel
    {
        Task<TEntity> AddAsync(TEntity entity, bool save = false);

        Task AddAsync(IEnumerable<TEntity> entities, bool save = false);

        Task UpdateAsync(TEntity entity, bool save = false);
        Task UpdateAsync(IEnumerable<TEntity> entities, bool save = false);

        Task RemoveAsync(TEntity entity, bool save = false);
        Task RemoveAsync(IEnumerable<TEntity> entities, bool save = false);

        Task SaveChangesAsync();
    }
}