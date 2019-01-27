using System.Collections.Generic;
using EducationSystem.Models.Source;

namespace EducationSystem.Managers.Interfaces.Source.Rest
{
    /// <summary>
    /// Интерфейс менеджера по работе с пользователями.
    /// </summary>
    public interface IManagerUser
    {
        /// <summary>
        /// Возвращает список всех пользователей.
        /// </summary>
        /// <returns>Список всех пользователей.</returns>
        List<User> GetAll();

        /// <summary>
        /// Возвращает пользователя по указанному идентификатору.
        /// </summary>
        /// <returns>Пользователь.</returns>
        User GetById(int id);
    }
}