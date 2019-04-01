using System.Text;

namespace EducationSystem.Models
{
    public class TokenParameters
    {
        public int LifeTimeInMinutes { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string SecretKey { get; set; }

        public byte[] SecretKeyInBytes => Encoding.ASCII.GetBytes(SecretKey);
    }
}