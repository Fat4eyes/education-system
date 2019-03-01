using System.Collections.Generic;
using EducationSystem.Database.Models.Source;
using EducationSystem.Models.Source.Filters;

namespace EducationSystem.Repositories.Interfaces.Source.Rest
{
    public interface IRepositoryTestResult : IRepositoryReadOnly<DatabaseTestResult>
    {
        (int Count, List<DatabaseTestResult> TestResults) GetTestResults(FilterTestResult filter);
        (int Count, List<DatabaseTestResult> TestResults) GetTestResultsByStudentId(int studentId, FilterTestResult filter);
    }
}