using System.Collections.Generic;

namespace EducationSystem.Models.Source.Rest
{
    public class Student : User
    {
        public Group Group { get; set; }

        public List<TestResult> TestResults { get; set; }
    }
}