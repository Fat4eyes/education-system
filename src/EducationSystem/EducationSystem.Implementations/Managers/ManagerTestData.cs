using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EducationSystem.Exceptions.Helpers;
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
        private readonly IRepositoryTest _repositoryTest;

        public ManagerTestData(
            IMapper mapper,
            ILogger<ManagerTestData> logger,
            IHelperUserRole helperUserRole,
            IRepositoryTest repositoryTest)
            : base(mapper, logger)
        {
            _helperUserRole = helperUserRole;
            _repositoryTest = repositoryTest;
        }

        public List<TestData> GetTestsDataForStudentByTestIds(int[] testIds, int studentId)
        {
            if (testIds == null)
                throw new ArgumentNullException(nameof(testIds));

            return testIds
                .Select(x => GetTestDataForStudentByTestId(x, studentId))
                .ToList();
        }

        public TestData GetTestDataForStudentByTestId(int testId, int studentId)
        {
            _helperUserRole.CheckRoleStudent(studentId);

            var test = _repositoryTest.GetTestForStudentById(testId, studentId) ??
                throw ExceptionHelper.CreateNotFoundException(
                    $"Тест не найден. Идентификатор теста: {testId}.",
                    $"Тест не найден.");

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