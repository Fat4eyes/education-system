using System.Collections.Generic;
using EducationSystem.Models.Source.Rest.Base;

namespace EducationSystem.Models.Source.Rest
{
    public class User : Model
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public bool Active { get; set; }

        public Group Group { get; set; }

        public List<Role> Roles { get; set; }

        public List<TestResult> TestResults { get; set; }
    }
}