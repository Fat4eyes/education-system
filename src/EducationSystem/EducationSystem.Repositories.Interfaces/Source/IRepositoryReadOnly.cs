using EducationSystem.Database.Models.Source.Base;

namespace EducationSystem.Repositories.Interfaces.Source
{
    public interface IRepositoryReadOnly<out TModel> where TModel : DatabaseModel
    {
        TModel GetById(int id);
    }
}