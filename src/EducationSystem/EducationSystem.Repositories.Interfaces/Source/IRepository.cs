using System.Threading.Tasks;
using EducationSystem.Database.Models.Source.Base;

namespace EducationSystem.Repositories.Interfaces.Source
{
    /// <summary>
    /// Интерфейс репозитория.
    /// </summary>
    public interface IRepository<TModel> : IReadOnlyRepository<TModel> where TModel : DatabaseModel
    {
        /// <summary>
        /// Добавляет новую запись (модель).
        /// </summary>
        /// <param name="model">Модель для добавления.</param>
        TModel Add(TModel model);

        /// <summary>
        /// Добавляет новую запись (модель).
        /// </summary>
        /// <param name="model">Модель для добавления.</param>
        Task<TModel> AddAsync(TModel model);

        /// <summary>
        /// Обновляет существующую запись (модель).
        /// </summary>
        TModel Update(TModel model);

        /// <summary>
        /// Удаляет запись (модель) по идентификатору.
        /// </summary>
        void Delete(int id);

        /// <summary>
        /// Удаляет запись (модель).
        /// </summary>
        void Delete(TModel model);

        /// <summary>
        /// Сохраняет изменения.
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Сохраняет изменения.
        /// </summary>
        Task SaveChangesAsync();
    }
}