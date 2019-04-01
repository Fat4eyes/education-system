using EducationSystem.Database.Models.Basics;

namespace EducationSystem.Repositories.Interfaces.Basics
{
    public interface IRepositoryReadOnly<out TModel> where TModel : DatabaseModel
    {
        TModel GetById(int id);
    }
}