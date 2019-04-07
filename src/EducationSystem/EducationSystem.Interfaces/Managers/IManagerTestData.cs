using EducationSystem.Models.Datas;

namespace EducationSystem.Interfaces.Managers
{
    public interface IManagerTestData
    {
        TestData GetTestDataForStudentByTestId(int testId, int studentId);
    }
}
