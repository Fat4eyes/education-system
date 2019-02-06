using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;

namespace EducationSystem.Managers.Interfaces.Source.Rest
{
    public interface IManagerTestResult
    {
        PagedData<TestResult> GetTests(OptionsTestResult options);

        PagedData<TestResult> GetTestResultsByUserId(int userId, OptionsTestResult options);

        TestResult GetTestResultById(int id, OptionsTestResult options);
    }
}