using System.Threading.Tasks;
using EducationSystem.Database.Models;
using EducationSystem.Exceptions;
using EducationSystem.Implementations.Managers;
using EducationSystem.Interfaces.Managers;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models.Options;
using EducationSystem.Models.Rest;
using EducationSystem.Repositories.Interfaces;
using Moq;
using Xunit;

namespace EducationSystem.Tests.Managers.Rest
{
    public class TestsManagerTest : TestsManager<ManagerTest>
    {
        private readonly IManagerTest _managerTest;

        private readonly Mock<IRepositoryTest> _mockRepositoryTest
            = new Mock<IRepositoryTest>();

        private readonly Mock<IRepositoryTestTheme> _mockRepositoryTestTheme
            = new Mock<IRepositoryTestTheme>();

        private readonly Mock<IValidator<Test>> _mockValidatorTest
            = new Mock<IValidator<Test>>();

        public TestsManagerTest()
        {
            _managerTest = new ManagerTest(
                Mapper,
                LoggerMock.Object,
                _mockValidatorTest.Object,
                MockExceptionFactory.Object,
                _mockRepositoryTest.Object,
                _mockRepositoryTestTheme.Object);
        }

        [Fact]
        public async Task GetTest_Found()
        {
            _mockRepositoryTest
                .Setup(x => x.GetByIdAsync(999))
                .ReturnsAsync(new DatabaseTest { Subject = "Subject" });

            var test = await _managerTest.GetTestAsync(999, new OptionsTest());

            Assert.Equal("Subject", test.Subject);
        }

        [Fact]
        public async Task GetTest_NotFound()
        {
            _mockRepositoryTest
                .Setup(x => x.GetByIdAsync(999))
                .ReturnsAsync((DatabaseTest) null);

            MockExceptionFactory
                .Setup(x => x.NotFound<DatabaseTest>(It.IsAny<int>()))
                .Returns(new EducationSystemNotFoundException());

            await Assert.ThrowsAsync<EducationSystemNotFoundException>(
                () => _managerTest.GetTestAsync(999, new OptionsTest()));
        }
    }
}