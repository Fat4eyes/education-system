using EducationSystem.Models;

namespace EducationSystem.Interfaces.Managers
{
    public interface IManagerTestExecution
    {
        TestExecution GetStudentTestExecution(int testId, int studentId);
    }
}