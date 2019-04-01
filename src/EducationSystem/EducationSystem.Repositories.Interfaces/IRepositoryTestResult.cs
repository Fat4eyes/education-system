using System.Collections.Generic;
using EducationSystem.Database.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Repositories.Interfaces.Basics;

namespace EducationSystem.Repositories.Interfaces
{
    public interface IRepositoryTestResult : IRepositoryReadOnly<DatabaseTestResult>
    {
        (int Count, List<DatabaseTestResult> TestResults) GetTestResults(FilterTestResult filter);
        (int Count, List<DatabaseTestResult> TestResults) GetTestResultsByStudentId(int studentId, FilterTestResult filter);
    }
}