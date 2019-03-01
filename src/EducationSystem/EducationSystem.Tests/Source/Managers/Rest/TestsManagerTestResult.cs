using EducationSystem.Exceptions.Source;
using EducationSystem.Managers.Implementations.Source.Rest;
using EducationSystem.Managers.Interfaces.Source.Rest;
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
                MockUserHelper.Object,
                MockRepositoryTestResult.Object);
        }

        [Fact]
        public void GetTestResultsByStudentId_NotStudent()
        {
            MockUserHelper
                .Setup(x => x.IsStudent(999))
                .Returns(false);

            Assert.Throws<EducationSystemException>(
                () => ManagerTestResult.GetTestResultsByStudentId(999, new OptionsTestResult(), new FilterTestResult()));
        }
    }
}