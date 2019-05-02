using System.Collections.Generic;
using System.Threading.Tasks;
using EducationSystem.Database.Models.Basics;

namespace EducationSystem.Interfaces.Repositories.Basics
{
    public interface IRepository<TModel> : IRepositoryReadOnly<TModel> where TModel : DatabaseModel
    {
        Task<TModel> AddAsync(TModel model, bool save = false);

        Task AddAsync(IEnumerable<TModel> models, bool save = false);

        Task UpdateAsync(TModel model, bool save = false);
        Task UpdateAsync(IEnumerable<TModel> models, bool save = false);

        Task RemoveAsync(int id, bool save = false);
        Task RemoveAsync(TModel model, bool save = false);
        Task RemoveAsync(IEnumerable<TModel> models, bool save = false);

        Task SaveChangesAsync();
    }
}