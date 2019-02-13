using System.Collections.Generic;
using EducationSystem.Database.Models.Source;
using EducationSystem.Models.Source.Options;

namespace EducationSystem.Repositories.Interfaces.Source.Rest
{
    public interface IRepositoryTestResult : IRepositoryReadOnly<DatabaseTestResult>
    {
        (int Count, List<DatabaseTestResult> TestResults) GetTestResults(OptionsTestResult options);

        (int Count, List<DatabaseTestResult> TestResults) GetTestResultsByStudentId(int studentId, OptionsTestResult options);

        DatabaseTestResult GetTestResultById(int id, OptionsTestResult options);
    }
}