using System.Collections.Generic;

namespace EducationSystem.Models.Source
{
    public class SignInResponse
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }
}