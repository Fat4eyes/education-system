using System.Collections.Generic;
using EducationSystem.Database.Models.Basics;

namespace EducationSystem.Repositories.Interfaces.Basics
{
    public interface IRepositoryReadOnly<TModel> where TModel : DatabaseModel
    {
        TModel GetById(int id);

        List<TModel> GetByIds(int[] ids);
    }
}