using System.Text;

namespace EducationSystem.Constants.Source
{
    public static class TokenParameters
    {
        public const string Publisher = "Education-System-Publisher";

        public const string Consumer = "Education-System-Consumer";

        public const string SecretKey = "Education-System-Secret-Key";

        public static byte[] SecretKeyInBytes { get; }
            = Encoding.ASCII.GetBytes(SecretKey);
    }
}