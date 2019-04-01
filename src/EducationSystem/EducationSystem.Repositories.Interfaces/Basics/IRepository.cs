using System.Collections.Generic;
using System.Threading.Tasks;
using EducationSystem.Database.Models.Basics;

namespace EducationSystem.Repositories.Interfaces.Basics
{
    public interface IRepository<TModel> : IRepositoryReadOnly<TModel> where TModel : DatabaseModel
    {
        TModel Add(TModel model, bool save = false);
        Task<TModel> AddAsync(TModel model, bool save = false);

        void Add(IEnumerable<TModel> models, bool save = false);
        Task AddAsync(IEnumerable<TModel> models, bool save = false);

        void Update(TModel model, bool save = false);
        Task UpdateAsync(TModel model, bool save = false);

        void Update(IEnumerable<TModel> models, bool save = false);
        Task UpdateAsync(IEnumerable<TModel> models, bool save = false);

        void Remove(int id, bool save = false);
        Task RemoveAsync(int id, bool save = false);

        void Remove(TModel model, bool save = false);
        Task RemoveAsync(TModel model, bool save = false);

        void Remove(IEnumerable<TModel> models, bool save = false);
        Task RemoveAsync(IEnumerable<TModel> models, bool save = false);

        void SaveChanges();
        Task SaveChangesAsync();
    }
}