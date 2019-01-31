using EducationSystem.Models.Source.Rest;

namespace EducationSystem.Models.Source
{
    public class SignInResponse
    {
        /// <summary>
        /// Токен.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Электронна почта (E-mail).
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Пользователь.
        /// </summary>
        public UserShort User { get; set; }
    }
}