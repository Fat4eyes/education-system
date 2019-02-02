using System.Collections.Generic;
using EducationSystem.Models.Source.Rest;

namespace EducationSystem.Managers.Interfaces.Source.Rest
{
    /// <summary>
    /// Интерфейс менеджера по работе с ролями пользователя.
    /// </summary>
    public interface IManagerRole
    {
        /// <summary>
        /// Возвращает список всех ролей.
        /// </summary>
        /// <returns>Список всех ролей.</returns>
        List<Role> GetAll();

        /// <summary>
        /// Возвращает роль по указанному идентификатору.
        /// </summary>
        /// <returns>Роль.</returns>
        Role GetById(int id);
    }
}