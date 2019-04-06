using EducationSystem.Models;
using EducationSystem.Models.Options;

namespace EducationSystem.Interfaces.Managers
{
    public interface IManagerTestExecution
    {
        TestExecution GetStudentTestExecution(int testId, int studentId, OptionsTestExecution options);
    }
}