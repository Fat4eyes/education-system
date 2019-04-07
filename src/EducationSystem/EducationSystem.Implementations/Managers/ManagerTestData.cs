using System.Linq;
using AutoMapper;
using EducationSystem.Exceptions.Helpers;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Managers;
using EducationSystem.Models.Datas;
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

        public TestData GetTestDataForStudentByTestId(int testId, int studentId)
        {
            _helperUserRole.CheckRoleStudent(studentId);

            var test = _repositoryTest.GetTestForStudentById(testId, studentId) ??
                throw ExceptionHelper.CreateNotFoundException(
                    $"Тест не найден. Идентификатор теста: {testId}.",
                    $"Тест не найден.");

            var themes = test.TestThemes
                .Select(x => x.Theme)
                .ToList();

            var questions = themes
                .SelectMany(x => x.Questions)
                .ToList();

            return new TestData(themes.Count, questions.Count);
        }
    }
}