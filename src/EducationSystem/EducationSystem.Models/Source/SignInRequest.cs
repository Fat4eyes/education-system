namespace EducationSystem.Models.Source
{
    public class SignInRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public bool Remember { get; set; }
    }
}