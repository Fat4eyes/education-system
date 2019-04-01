using EducationSystem.Exceptions.Source;
using EducationSystem.Implementations.Managers.Rest;
using EducationSystem.Interfaces.Managers.Rest;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Models.Source.Options;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Moq;
using Xunit;

namespace EducationSystem.Tests.Source.Managers.Rest
{
    public class TestsManagerTestResult : TestsManager<ManagerTestResult>
    {
        protected IManagerTestResult ManagerTestResult { get; }

        protected Mock<IRepositoryTestResult> MockRepositoryTestResult { get; set; }

        public TestsManagerTestResult()
        {
            MockRepositoryTestResult = new Mock<IRepositoryTestResult>();

            ManagerTestResult = new ManagerTestResult(
                Mapper,
                LoggerMock.Object,
                MockHelperUser.Object,
                MockRepositoryTestResult.Object);
        }

        [Fact]
        public void GetTestResultsByStudentId_NotStudent()
        {
            MockHelperUser
                .Setup(x => x.CheckRoleStudent(999))
                .Throws<EducationSystemException>();

            Assert.Throws<EducationSystemException>(
                () => ManagerTestResult.GetTestResultsByStudentId(999, new OptionsTestResult(), new FilterTestResult()));
        }
    }
}