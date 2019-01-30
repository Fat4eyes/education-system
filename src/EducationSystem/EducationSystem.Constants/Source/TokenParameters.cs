using System.Text;

namespace EducationSystem.Constants.Source
{
    public static class TokenParameters
    {
        public const string Issuer = "Education-System-Issuer";

        public const string Audience = "Education-System-Audience";

        public const string SecretKey = "Education-System-Secret-Key";

        public static byte[] SecretKeyInBytes { get; }
            = Encoding.ASCII.GetBytes(SecretKey);
    }
}