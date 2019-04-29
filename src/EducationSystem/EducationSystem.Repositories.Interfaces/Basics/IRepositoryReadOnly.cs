using System.Collections.Generic;
using System.Threading.Tasks;
using EducationSystem.Database.Models.Basics;

namespace EducationSystem.Repositories.Interfaces.Basics
{
    public interface IRepositoryReadOnly<TModel> where TModel : DatabaseModel
    {
        Task<TModel> GetById(int id);

        Task<List<TModel>> GetByIds(int[] ids);
    }
}