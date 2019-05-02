using System.Collections.Generic;
using System.Linq;
using EducationSystem.Constants;
using EducationSystem.Models.Rest.Basics;

namespace EducationSystem.Models.Rest
{
    public class User : Model
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public bool Active { get; set; }

        public List<Role> Roles { get; set; }

        public Group Group { get; set; }

        public bool IsStudent() => HasRole(UserRoles.Student);

        public bool IsNotStudent() => !IsStudent();

        public bool IsAdmin() => HasRole(UserRoles.Admin);

        public bool IsNotAdmin() => !IsAdmin();

        public bool IsLecturer() => HasRole(UserRoles.Lecturer);

        public bool IsNotLecturer() => !IsLecturer();

        private bool HasRole(string role)
        {
            return Roles
                .Where(x => !string.IsNullOrWhiteSpace(x.Name))
                .Select(x => x.Name.Trim().ToLowerInvariant())
                .Contains(role.ToLowerInvariant());
        }
    }
}