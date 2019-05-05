namespace EducationSystem.Models
{
    public class TokenRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public TokenRequest() { }

        public TokenRequest(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}