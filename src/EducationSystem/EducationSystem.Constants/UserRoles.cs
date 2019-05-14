using System;

namespace EducationSystem.Constants
{
    public static class UserRoles
    {
        public const string Admin = nameof(Admin);
        public const string Student = nameof(Student);
        public const string Lecturer = nameof(Lecturer);
        public const string Employee = nameof(Employee);

        public static bool IsStudentRole(string name) =>
            string.Equals(Student, name, StringComparison.InvariantCultureIgnoreCase);

        public static bool IsAdminRole(string name) =>
            string.Equals(Admin, name, StringComparison.InvariantCultureIgnoreCase);

        public static bool IsLecturerRole(string name) =>
            string.Equals(Lecturer, name, StringComparison.InvariantCultureIgnoreCase);

        public static readonly string[] All =
        {
            Admin,
            Student,
            Lecturer
        };
    }
}