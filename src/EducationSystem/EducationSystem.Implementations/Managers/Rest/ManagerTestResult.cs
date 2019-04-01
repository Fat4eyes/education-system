using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EducationSystem.Database.Models.Source;
using EducationSystem.Exceptions.Source.Helpers;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Managers.Rest;
using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;
using EducationSystem.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Managers.Rest
{
    public sealed class ManagerTestResult : Manager<ManagerTestResult>, IManagerTestResult
    {
        private readonly IHelperUser _helperUser;
        private readonly IRepositoryTestResult _repositoryTestResult;

        public ManagerTestResult(
            IMapper mapper,
            ILogger<ManagerTestResult> logger,
            IHelperUser helperUser,
            IRepositoryTestResult repositoryTestResult)
            : base(mapper, logger)
        {
            _helperUser = helperUser;
            _repositoryTestResult = repositoryTestResult;
        }

        public PagedData<TestResult> GetTests(OptionsTestResult options, FilterTestResult filter)
        {
            var (count, testResults) = _repositoryTestResult.GetTestResults(filter);

            return new PagedData<TestResult>(testResults.Select(x => Map(x, options)).ToList(), count);
        }

        public PagedData<TestResult> GetTestResultsByStudentId(int studentId, OptionsTestResult options, FilterTestResult filter)
        {
            _helperUser.CheckRoleStudent(studentId);

            var (count, testResults) = _repositoryTestResult.GetTestResultsByStudentId(studentId, filter);

            return new PagedData<TestResult>(testResults.Select(x => Map(x, options)).ToList(), count);
        }

        public TestResult GetTestResultById(int id, OptionsTestResult options)
        {
            var testResult = _repositoryTestResult.GetById(id) ??
                throw ExceptionHelper.CreateNotFoundException(
                    $"Результат теста не найден. Идентификатор: {id}.",
                    $"Результат теста не найден.");

            return Map(testResult, options);
        }

        private TestResult Map(DatabaseTestResult testResult, OptionsTestResult options)
        {
            return Mapper.Map<DatabaseTestResult, TestResult>(testResult, x =>
            {
                x.AfterMap((s, d) =>
                {
                    if (options.WithTest)
                        d.Test = Mapper.Map<Test>(s.Test);

                    if (options.WithGivenAnswers)
                        d.GivenAnswers = Mapper.Map<List<GivenAnswer>>(s.GivenAnswers);
                });
            });
        }
    }
}