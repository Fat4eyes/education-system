using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;

namespace EducationSystem.Managers.Interfaces.Source.Rest
{
    public interface IManagerTestResult
    {
        PagedData<TestResult> GetTests(OptionsTestResult options, Filter filter);
        PagedData<TestResult> GetTestResultsByStudentId(int studentId, OptionsTestResult options, Filter filter);

        TestResult GetTestResultById(int id, OptionsTestResult options);
    }
}