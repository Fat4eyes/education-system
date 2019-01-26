using System.Linq;
using System.Collections.Generic;
using EducationSystem.Database.Models.Source;

namespace EducationSystem.Repositories.Interfaces.Source
{
    public interface IRepositoryReadOnly<out TModel> where TModel : DatabaseModel
    {
        IQueryable<TModel> AsQueryable();

        IEnumerable<TModel> GetAll();

        TModel GetById(int id);
    }
}