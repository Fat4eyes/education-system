using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EducationSystem.Constants.Source;
using EducationSystem.Database.Models.Source;
using EducationSystem.Exceptions.Source.Helpers;
using EducationSystem.Helpers.Interfaces.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Managers.Implementations.Source.Rest
{
    public class ManagerTestResult : Manager<ManagerTestResult>, IManagerTestResult
    {
        protected IUserHelper UserHelper { get; }
        protected IRepositoryTestResult RepositoryTestResult { get; }

        public ManagerTestResult(
            IMapper mapper,
            ILogger<ManagerTestResult> logger,
            IUserHelper userHelper,
            IRepositoryTestResult repositoryTestResult)
            : base(mapper, logger)
        {
            UserHelper = userHelper;
            RepositoryTestResult = repositoryTestResult;
        }

        public PagedData<TestResult> GetTests(OptionsTestResult options, Filter filter)
        {
            var (count, testResults) = RepositoryTestResult.GetTestResults(filter);

            return new PagedData<TestResult>(testResults.Select(x => Map(x, options)).ToList(), count);
        }

        public PagedData<TestResult> GetTestResultsByStudentId(int studentId, OptionsTestResult options, Filter filter)
        {
            if (!UserHelper.IsStudent(studentId))
                throw ExceptionHelper.CreateException(
                    Messages.User.NotStudent(studentId),
                    Messages.User.NotStudentPublic);

            var (count, testResults) = RepositoryTestResult.GetTestResultsByStudentId(studentId, filter);

            return new PagedData<TestResult>(testResults.Select(x => Map(x, options)).ToList(), count);
        }

        public TestResult GetTestResultById(int id, OptionsTestResult options)
        {
            var testResult = RepositoryTestResult.GetTestResultById(id) ??
                throw ExceptionHelper.CreateNotFoundException(
                    Messages.TestResult.NotFoundById(id),
                    Messages.TestResult.NotFoundPublic);

            return Mapper.Map<TestResult>(Map(testResult, options));
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