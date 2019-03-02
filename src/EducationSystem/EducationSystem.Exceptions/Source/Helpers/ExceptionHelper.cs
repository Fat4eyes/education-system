namespace EducationSystem.Exceptions.Source.Helpers
{
    public static class ExceptionHelper
    {
        public static EducationSystemException CreateException(string @private, string @public)
            => new EducationSystemException(@private, CreatePublicException(@public));

        public static EducationSystemNotFoundException CreateNotFoundException(string @private, string @public)
            => new EducationSystemNotFoundException(@private, CreatePublicException(@public));

        public static EducationSystemPublicException CreatePublicException(string @public)
            => new EducationSystemPublicException(@public);

        public static EducationSystemException CreateException(string @private)
            => new EducationSystemException(@private);

    }
}