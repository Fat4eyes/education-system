namespace EducationSystem.Models.Source
{
    /// <summary>
    /// Запрос на вход (получение токена).
    /// </summary>
    public class SignInRequest
    {
        /// <summary>
        /// Электронная почта.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Признак того, что необходимо запомнить пользователя (увеличивается время жизни токена).
        /// </summary>
        public bool Remember { get; set; }
    }
}