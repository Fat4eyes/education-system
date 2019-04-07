using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Managers;
using EducationSystem.Models;
using EducationSystem.Models.Options;

namespace EducationSystem.Implementations.Managers
{
    public sealed class ManagerTestExecution : IManagerTestExecution
    {
        private readonly IHelperUserRole _helperUserRole;

        private readonly IManagerTestData _managerTestData;
        private readonly IManagerQuestion _managerQuestion;

        public ManagerTestExecution(
            IHelperUserRole helperUserRole,
            IManagerTestData managerTestData,
            IManagerQuestion managerQuestion)
        {
            _managerTestData = managerTestData;
            _managerQuestion = managerQuestion;
            _helperUserRole = helperUserRole;
        }

        public TestExecution GetStudentTestExecution(int testId, int studentId, OptionsTestExecution options)
        {
            _helperUserRole.CheckRoleStudent(studentId);

            var testData = _managerTestData.GetTestDataForStudentByTestId(testId, studentId);

            var questions = _managerQuestion.GetQuestionsForStudentByTestId(testId, studentId, options.TestSize);

            return new TestExecution(testData, questions);
        }
    }
}