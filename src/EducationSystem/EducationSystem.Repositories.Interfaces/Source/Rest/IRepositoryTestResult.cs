using System.Collections.Generic;
using EducationSystem.Database.Models.Source;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Models.Source.Options;

namespace EducationSystem.Repositories.Interfaces.Source.Rest
{
    public interface IRepositoryTestResult : IRepositoryReadOnly<DatabaseTestResult>
    {
        (int Count, List<DatabaseTestResult> TestResults) GetTestResults(Filter filter);

        (int Count, List<DatabaseTestResult> TestResults) GetTestResultsByStudentId(int studentId, Filter filter);

        DatabaseTestResult GetTestResultById(int id);
    }
}