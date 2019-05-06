using System.Collections.Generic;
using System.Threading.Tasks;
using EducationSystem.Database.Models.Basics;

namespace EducationSystem.Interfaces.Repositories
{
    public interface IRepository<TModel> : IRepositoryReadOnly<TModel> where TModel : DatabaseModel
    {
        Task<TModel> AddAsync(TModel entity, bool save = false);

        Task AddAsync(IEnumerable<TModel> entities, bool save = false);

        Task UpdateAsync(TModel entity, bool save = false);
        Task UpdateAsync(IEnumerable<TModel> entities, bool save = false);

        Task RemoveAsync(TModel entity, bool save = false);
        Task RemoveAsync(IEnumerable<TModel> entities, bool save = false);

        Task SaveChangesAsync();
    }
}