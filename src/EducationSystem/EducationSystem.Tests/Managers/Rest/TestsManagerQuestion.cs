using EducationSystem.Exceptions;
using EducationSystem.Implementations.Managers;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Managers;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models.Rest;
using EducationSystem.Repositories.Interfaces;
using Moq;
using Xunit;

namespace EducationSystem.Tests.Managers.Rest
{
    public class TestsManagerQuestion : TestsManager<ManagerQuestion>
    {
        private readonly IManagerQuestion _managerQuestion;

        private readonly Mock<IHelperPath> _mockHelperPath
            = new Mock<IHelperPath>();

        private readonly Mock<IValidator<Question>> _mockHelperQuestion
            = new Mock<IValidator<Question>>();

        private readonly Mock<IRepositoryAnswer> _mockRepositoryAnswer
            = new Mock<IRepositoryAnswer>();

        private readonly Mock<IRepositoryProgram> _mockRepositoryProgram
            = new Mock<IRepositoryProgram>();

        private readonly Mock<IRepositoryQuestion> _mockRepositoryQuestion
            = new Mock<IRepositoryQuestion>();

        private readonly Mock<IRepositoryProgramData> _mockRepositoryProgramData
            = new Mock<IRepositoryProgramData>();

        public TestsManagerQuestion()
        {
            _managerQuestion = new ManagerQuestion(
                Mapper,
                LoggerMock.Object,
                _mockHelperPath.Object,
                MockHelperUser.Object,
                _mockHelperQuestion.Object,
                _mockRepositoryAnswer.Object,
                _mockRepositoryProgram.Object,
                _mockRepositoryQuestion.Object,
                _mockRepositoryProgramData.Object);
        }

        [Fact]
        public void GetQuestionsForStudentByTestId_NotStudent()
        {
            MockHelperUser
                .Setup(x => x.CheckRoleStudent(999))
                .Throws<EducationSystemException>();

            Assert.Throws<EducationSystemException>(
                () => _managerQuestion.GetQuestionsForStudentByTestId(999, 999));
        }
    }
}