using System.Collections.Generic;
using System.Threading.Tasks;
using EducationSystem.Database.Models.Source.Base;

namespace EducationSystem.Repositories.Interfaces.Source
{
    public interface IRepository<TModel> : IRepositoryReadOnly<TModel> where TModel : DatabaseModel
    {
        TModel Add(TModel model);
        Task<TModel> AddAsync(TModel model);

        void AddRange(IEnumerable<TModel> models);
        Task AddRangeAsync(IEnumerable<TModel> models);

        void Update(TModel model);
        void UpdateRange(IEnumerable<TModel> models);

        void Remove(int id);
        void Remove(TModel model);
        void RemoveRange(IEnumerable<TModel> models);

        void SaveChanges();
        Task SaveChangesAsync();
    }
}