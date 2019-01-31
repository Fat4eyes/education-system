using System.Collections.Generic;
using EducationSystem.Models.Source.Rest;

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

        /// <summary>
        /// Возвращает пользователя по электронной почте и паролю.
        /// </summary>
        /// <param name="email">E-mail (электронная почта).</param>
        /// <param name="password">Пароль.</param>
        /// <returns>Пользователь.</returns>
        User GetByEmailAndPassword(string email, string password);
    }
}