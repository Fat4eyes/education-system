using System.Linq;
using System.Collections.Generic;
using EducationSystem.Database.Models.Source.Base;

namespace EducationSystem.Repositories.Interfaces.Source
{
    public interface IRepositoryReadOnly<TModel> where TModel : DatabaseModel
    {
        IQueryable<TModel> AsQueryable();

        IEnumerable<TModel> GetAll();

        TModel GetById(int id);

        void Attach(TModel model);
    }
}