using System.Threading.Tasks;
using EducationSystem.Database.Models.Source.Base;

namespace EducationSystem.Repositories.Interfaces.Source
{
    public interface IRepository<TModel> : IReadOnlyRepository<TModel> where TModel : DatabaseModel
    {
        TModel Add(TModel model);

        Task<TModel> AddAsync(TModel model);

        TModel Update(TModel model);

        void Delete(int id);

        void Delete(TModel model);

        void SaveChanges();

        Task SaveChangesAsync();
    }
}