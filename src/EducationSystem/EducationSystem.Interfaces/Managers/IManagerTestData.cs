using System.Collections.Generic;
using EducationSystem.Models.Datas;

namespace EducationSystem.Interfaces.Managers
{
    public interface IManagerTestData
    {
        List<TestData> GetTestsDataForStudentByTestIds(int[] testIds, int studentId);

        TestData GetTestDataForStudentByTestId(int testId, int studentId);
    }
}