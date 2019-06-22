namespace EducationSystem.Models
{
    public sealed class LogInRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string Address { get; set; }

        public static implicit operator TokenRequest(LogInRequest request)
        {
            return new TokenRequest(request.Email, request.Password);
        }
    }
}