using System;

namespace EducationSystem.Exceptions.Source
{
    public class EducationSystemPublicException : Exception
    {
        public EducationSystemPublicException() { }

        public EducationSystemPublicException(string message)
            : base(message) { }

        public EducationSystemPublicException(string message, Exception inner)
            : base(message, inner) { }
    }
}