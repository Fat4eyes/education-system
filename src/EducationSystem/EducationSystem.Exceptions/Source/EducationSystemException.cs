using System;

namespace EducationSystem.Exceptions.Source
{
    public class EducationSystemException : Exception
    {
        public EducationSystemException() { }

        public EducationSystemException(string message)
            : base(message) { }

        public EducationSystemException(string message, Exception inner)
            : base(message, inner) { }
    }
}