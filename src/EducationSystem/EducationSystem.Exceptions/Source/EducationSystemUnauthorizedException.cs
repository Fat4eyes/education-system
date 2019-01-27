using System;

namespace EducationSystem.Exceptions.Source
{
    public class EducationSystemUnauthorizedException : Exception
    {
        public EducationSystemUnauthorizedException() { }

        public EducationSystemUnauthorizedException(string message)
            : base(message) { }

        public EducationSystemUnauthorizedException(string message, Exception inner)
            : base(message, inner) { }
    }
}