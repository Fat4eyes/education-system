using System.Collections.Generic;
using System.Linq;
using EducationSystem.Database.Contexts;
using EducationSystem.Database.Models;
using EducationSystem.Extensions;
using EducationSystem.Models.Filters;
using EducationSystem.Repositories.Implementations.Basics;
using EducationSystem.Repositories.Interfaces;

namespace EducationSystem.Repositories.Implementations
{
    public sealed class RepositoryTestResult : RepositoryReadOnly<DatabaseTestResult>, IRepositoryTestResult
    {
        public RepositoryTestResult(DatabaseContext context)
            : base(context) { }

        public (int Count, List<DatabaseTestResult> TestResults) GetTestResults(FilterTestResult filter) =>
            AsQueryable().ApplyPaging(filter);

        public (int Count, List<DatabaseTestResult> TestResults) GetTestResultsByStudentId(int studentId, FilterTestResult filter)
        {
            return AsQueryable()
                .Where(x => x.UserId == studentId)
                .ApplyPaging(filter);
        }
    }
}