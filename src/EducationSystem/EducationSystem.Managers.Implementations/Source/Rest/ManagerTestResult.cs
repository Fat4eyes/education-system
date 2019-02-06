﻿using System.Collections.Generic;
using AutoMapper;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Managers.Implementations.Source.Rest
{
    public class ManagerTestResult : Manager<ManagerTestResult>, IManagerTestResult
    {
        protected IRepositoryTestResult RepositoryTestResult { get; }

        public ManagerTestResult(
            IMapper mapper,
            ILogger<ManagerTestResult> logger,
            IRepositoryTestResult repositoryTestResult)
            : base(mapper, logger)
        {
            RepositoryTestResult = repositoryTestResult;
        }

        public PagedData<TestResult> GetTestResultsByUserId(int userId, OptionsTestResult options)
        {
            var (count, testResults) = RepositoryTestResult.GetTestResultByUserId(userId, options);

            return new PagedData<TestResult>(Mapper.Map<List<TestResult>>(testResults), count);
        }
    }
}