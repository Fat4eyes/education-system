using System.Collections.Generic;
using System.Threading.Tasks;
using EducationSystem.Database.Models.Basics;

namespace EducationSystem.Repositories.Interfaces.Basics
{
    public interface IRepositoryReadOnly<TModel> where TModel : DatabaseModel
    {
        Task<TModel> GetByIdAsync(int id);

        Task<List<TModel>> GetByIdsAsync(int[] ids);
    }
}