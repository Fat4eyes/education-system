using EducationSystem.Exceptions;
using EducationSystem.Implementations.Managers.Rest;
using EducationSystem.Interfaces.Managers.Rest;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.Repositories.Interfaces;
using Moq;
using Xunit;

namespace EducationSystem.Tests.Managers.Rest
{
    public class TestsManagerTestResult : TestsManager<ManagerTestResult>
    {
        private readonly IManagerTestResult _managerTestResult;

        private readonly Mock<IRepositoryTestResult> _mockRepositoryTestResult
            = new Mock<IRepositoryTestResult>();

        public TestsManagerTestResult()
        {
            _managerTestResult = new ManagerTestResult(
                Mapper,
                LoggerMock.Object,
                MockHelperUser.Object,
                _mockRepositoryTestResult.Object);
        }

        [Fact]
        public void GetTestResultsByStudentId_NotStudent()
        {
            MockHelperUser
                .Setup(x => x.CheckRoleStudent(999))
                .Throws<EducationSystemException>();

            Assert.Throws<EducationSystemException>(
                () => _managerTestResult.GetTestResultsByStudentId(999, new OptionsTestResult(), new FilterTestResult()));
        }
    }
}