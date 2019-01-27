namespace EducationSystem.Database.Source
{
    /// <summary>
    /// Параметры для подключения к базе данных.
    /// </summary>
    public class Database
    {
        /// <summary>
        /// Название базы данных.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Сервер (хост).
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Порт.
        /// </summary>
        public string Port { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        public string UserPassword { get; set; }
    }
}