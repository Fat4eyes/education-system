﻿using EducationSystem.Database.Models.Source;

namespace EducationSystem.Repositories.Interfaces.Source.Rest
{
    /// <summary>
    /// Интерфейс репозитория для модели <see cref="DatabaseUser" />.
    /// </summary>
    public interface IUserRepository : IReadOnlyRepository<DatabaseUser>
    {
        /// <summary>
        /// Возвращает пользователя по электронной почте.
        /// </summary>
        /// <param name="email">E-mail (электронная почта).</param>
        /// <returns>Пользователь.</returns>
        DatabaseUser GetByEmail(string email);
    }
}