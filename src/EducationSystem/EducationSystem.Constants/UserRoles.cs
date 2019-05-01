namespace EducationSystem.Constants
{
    public static class UserRoles
    {
        public const string Admin = nameof(Admin);
        public const string Student = nameof(Student);
        public const string Lecturer = nameof(Lecturer);

        public static readonly string[] All =
        {
            Admin,
            Student,
            Lecturer
        };
    }
}