using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Rest;

namespace EducationSystem.Interfaces.Managers.Rest
{
    public interface IManagerTestResult
    {
        PagedData<TestResult> GetTests(OptionsTestResult options, FilterTestResult filter);
        PagedData<TestResult> GetTestResultsByStudentId(int studentId, OptionsTestResult options, FilterTestResult filter);

        TestResult GetTestResultById(int id, OptionsTestResult options);
    }
}