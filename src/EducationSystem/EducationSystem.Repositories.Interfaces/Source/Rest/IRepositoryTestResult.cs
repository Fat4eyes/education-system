using System.Collections.Generic;
using EducationSystem.Database.Models.Source;
using EducationSystem.Models.Source.Options;

namespace EducationSystem.Repositories.Interfaces.Source.Rest
{
    public interface IRepositoryTestResult : IRepositoryReadOnly<DatabaseTestResult>
    {
        (int Count, List<DatabaseTestResult> TestResults) GetTestResultByUserId(int userId, OptionsTestResult options);
    }
}