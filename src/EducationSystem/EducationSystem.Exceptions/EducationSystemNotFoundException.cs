using System;

namespace EducationSystem.Exceptions
{
    public class EducationSystemNotFoundException : Exception
    {
        public EducationSystemNotFoundException() { }

        public EducationSystemNotFoundException(string message)
            : base(message) { }

        public EducationSystemNotFoundException(string message, Exception inner)
            : base(message, inner) { }
    }
}