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
                _mockRepositoryTest.Object,
                _mockRepositoryTestTheme.Object);
        }

        [Fact]
        public async Task GetTest_Found()
        {
            _mockRepositoryTest
                .Setup(x => x.GetById(999))
                .Returns(new DatabaseTest { Subject = "Subject" });

            var test = await _managerTest.GetTest(999, new OptionsTest());

            Assert.Equal("Subject", test.Subject);
        }

        [Fact]
        public async Task GetTest_NotFound()
        {
            _mockRepositoryTest
                .Setup(x => x.GetById(999))
                .Returns((DatabaseTest) null);

            await Assert.ThrowsAsync<EducationSystemNotFoundException>(
                () => _managerTest.GetTest(999, new OptionsTest()));
        }
    }
}