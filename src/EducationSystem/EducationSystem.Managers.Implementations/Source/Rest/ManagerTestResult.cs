using System.Collections.Generic;
using AutoMapper;
using EducationSystem.Constants.Source;
using EducationSystem.Exceptions.Source;
using EducationSystem.Helpers.Interfaces.Source;
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

        public PagedData<TestResult> GetTests(OptionsTestResult options)
        {
            var (count, testResults) = RepositoryTestResult.GetTestResults(options);

            return new PagedData<TestResult>(Mapper.Map<List<TestResult>>(testResults), count);
        }

        public PagedData<TestResult> GetTestResultsByStudentId(int studentId, OptionsTestResult options)
        {
            if (!UserHelper.IsStudent(studentId))
                throw new EducationSystemNotFoundException(
                    string.Format(Messages.User.NotStudent, studentId),
                    new EducationSystemPublicException(Messages.User.NotStudentPublic));

            var (count, testResults) = RepositoryTestResult.GetTestResultsByStudentId(studentId, options);

            return new PagedData<TestResult>(Mapper.Map<List<TestResult>>(testResults), count);
        }

        public TestResult GetTestResultById(int id, OptionsTestResult options)
        {
            var testResult = RepositoryTestResult.GetTestResultById(id, options) ??
                 throw new EducationSystemException(
                     string.Format(Messages.TestResult.NotFoundById, id),
                     new EducationSystemPublicException(Messages.TestResult.NotFoundPublic));

            return Mapper.Map<TestResult>(testResult);
        }
    }
}