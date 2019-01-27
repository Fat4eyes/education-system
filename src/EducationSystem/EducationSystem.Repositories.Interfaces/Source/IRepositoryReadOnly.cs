using System.Linq;
using System.Collections.Generic;
using EducationSystem.Database.Models.Source;

namespace EducationSystem.Repositories.Interfaces.Source
{
    /// <summary>
    /// Интерфейс репозитория (только для получения данных).
    /// </summary>
    public interface IRepositoryReadOnly<out TModel> where TModel : DatabaseModel
    {
        /// <summary>
        /// Возвращает интерфейс <see cref="IQueryable{TModel}" /> для построения запроса. 
        /// </summary>
        /// <returns></returns>
        IQueryable<TModel> AsQueryable();

        /// <summary>
        /// Возвращает все записи (модели).
        /// </summary>
        IEnumerable<TModel> GetAll();

        /// <summary>
        /// Возвращает запись (модель) по идентификатору.
        /// </summary>
        TModel GetById(int id);
    }
}