using EducationSystem.Models.Source.Rest;

namespace EducationSystem.Models.Source
{
    public class SignInResponse
    {
        public string Token { get; set; }

        public UserShort User { get; set; }
    }
}