namespace EducationSystem.Exceptions.Source.Helpers
{
    public static class ExceptionHelper
    {
        public static EducationSystemException CreateException(string a, string b)
            => new EducationSystemException(a, CreatePublicException(b));

        public static EducationSystemNotFoundException CreateNotFoundException(string a, string b)
            => new EducationSystemNotFoundException(a, CreatePublicException(b));

        public static EducationSystemPublicException CreatePublicException(string message)
            => new EducationSystemPublicException(message);
    }
}