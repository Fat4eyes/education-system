namespace EducationSystem.Helpers.Interfaces.Source
{
    public interface IUserHelper
    {
        bool IsStudent(int userId);

        bool IsAdmin(int userId);

        bool IsLecturer(int userId);

        bool IsEmployee(int userId);
    }
}