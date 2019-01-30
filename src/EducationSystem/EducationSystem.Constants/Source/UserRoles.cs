namespace EducationSystem.Constants.Source
{
    /// <summary>
    /// Пользовательские роли.
    /// </summary>
    public static class UserRoles
    {
        /// <summary>
        /// Администратор.
        /// </summary>
        public const string Admin = nameof(Admin);

        /// <summary>
        /// Студент.
        /// </summary>
        public const string Student = nameof(Student);

        /// <summary>
        /// Преподаватель.
        /// </summary>
        public const string Lecturer = nameof(Lecturer);

        /// <summary>
        /// Cотрудник.
        /// </summary>
        public const string Employee = nameof(Employee);

        /// <summary>
        /// Администратор и Преподаватель.
        /// </summary>
        public const string AdminAndLecturer = Admin + "," + Lecturer;

        /// <summary>
        /// Администратор и Cотрудник.
        /// </summary>
        public const string AdminAndEmployee = Admin + "," + Employee;

        /// <summary>
        /// Преподаватель и Cотрудник.
        /// </summary>
        public const string LecturerAndEmployee = Lecturer + "," + Employee;

        /// <summary>
        /// Администратор и Преподаватель и Cотрудник.
        /// </summary>
        public const string AdminAndLecturerAndEmployee = Admin + "," + Lecturer + "," + Employee;

        /// <summary>
        /// Все.
        /// </summary>
        public const string All = Student + "," + Admin + "," + Lecturer + "," + Employee;
    }
}