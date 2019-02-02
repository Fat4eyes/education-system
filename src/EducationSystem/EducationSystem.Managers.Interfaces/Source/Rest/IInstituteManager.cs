using System.Collections.Generic;
using EducationSystem.Models.Source.Rest;

namespace EducationSystem.Managers.Interfaces.Source.Rest
{
    /// <summary>
    /// Интерфейс менеджера по работе с институтами.
    /// </summary>
    public interface IInstituteManager
    {
        /// <summary>
        /// Возвращает список всех институтов.
        /// </summary>
        /// <returns>Список всех институтов.</returns>
        List<Institute> GetAll();

        /// <summary>
        /// Возвращает институт по указанному идентификатору.
        /// </summary>
        /// <returns>Институт.</returns>
        Institute GetById(int id);
    }
}