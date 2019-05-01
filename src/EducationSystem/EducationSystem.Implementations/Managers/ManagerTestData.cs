using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Managers;
using EducationSystem.Models.Datas;
using EducationSystem.Models.Rest;
using EducationSystem.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Managers
{
    public sealed class ManagerTestData : Manager<ManagerTestData>, IManagerTestData
    {
        private readonly IHelperUserRole _helperUserRole;
        private readonly IExceptionFactory _exceptionFactory;
        private readonly IRepositoryTest _repositoryTest;

        public ManagerTestData(
            IMapper mapper,
            ILogger<ManagerTestData> logger,
            IHelperUserRole helperUserRole,
            IExceptionFactory exceptionFactory,
            IRepositoryTest repositoryTest)
            : base(mapper, logger)
        {
            _helperUserRole = helperUserRole;
            _exceptionFactory = exceptionFactory;
            _repositoryTest = repositoryTest;
        }

        public async Task<TestData> GetTestDataForStudentByTestIdAsync(int testId, int studentId)
        {
            await _helperUserRole.CheckRoleStudentAsync(studentId);

            var test = await _repositoryTest.GetTestForStudentByIdAsync(testId, studentId) ??
                throw _exceptionFactory.NotFound<DatabaseTest>(testId);

            var themes = test.TestThemes
                .Select(x => x.Theme)
                .ToArray();

            var questions = themes
                .SelectMany(x => x.Questions)
                .ToArray();

            return new TestData
            {
                Test = Mapper.Map<Test>(test),
                ThemesCount = themes.Length,
                QuestionsCount = questions.Length,
                PassedThemesCount = themes
                    .Count(x => x.Questions.All(y => y.QuestionStudents.Any(z => z.StudentId == studentId && z.Passed))),
                PassedQuestionsCount = questions
                    .Count(x => x.QuestionStudents.Any(y => y.StudentId == studentId && y.Passed))
            };
        }
    }
}