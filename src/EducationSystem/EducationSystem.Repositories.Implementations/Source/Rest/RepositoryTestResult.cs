﻿using System.Collections.Generic;
using System.Linq;
using EducationSystem.Database.Models.Source;
using EducationSystem.Database.Source.Contexts;
using EducationSystem.Extensions.Source;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Repositories.Interfaces.Source.Rest;

namespace EducationSystem.Repositories.Implementations.Source.Rest
{
    public class RepositoryTestResult : RepositoryReadOnly<DatabaseTestResult>, IRepositoryTestResult
    {
        public RepositoryTestResult(DatabaseContext context)
            : base(context) { }

        public (int Count, List<DatabaseTestResult> TestResults) GetTestResults(Filter filter) =>
            AsQueryable().ApplyPaging(filter);

        public (int Count, List<DatabaseTestResult> TestResults) GetTestResultsByStudentId(int studentId, Filter filter) =>
            AsQueryable()
                .Where(x => x.UserId == studentId)
                .ApplyPaging(filter);

        public DatabaseTestResult GetTestResultById(int id) => GetById(id);
    }
}