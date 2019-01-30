using EducationSystem.Database.Models.Source;

namespace EducationSystem.Repositories.Interfaces.Source.Rest
{
    /// <summary>
    /// Интерфейс репозитория для модели <see cref="DatabaseUser" />.
    /// </summary>
    public interface IRepositoryUser : IRepositoryReadOnly<DatabaseUser>
    {
        /// <summary>
        /// Возвращает пользователя по электронной почте и паролю.
        /// </summary>
        /// <param name="email">E-mail (электронная почта).</param>
        /// <param name="password">Пароль.</param>
        /// <returns>Пользователь.</returns>
        DatabaseUser GetByEmailAndPassword(string email, string password);
    }
}