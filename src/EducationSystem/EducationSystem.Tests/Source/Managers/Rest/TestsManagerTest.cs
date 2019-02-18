using EducationSystem.Database.Models.Source;
using EducationSystem.Exceptions.Source;
using EducationSystem.Managers.Implementations.Source.Rest;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source.Options;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Moq;
using Xunit;

namespace EducationSystem.Tests.Source.Managers.Rest
{
    public class TestsManagerTest : TestsManager<ManagerTest>
    {
        protected IManagerTest ManagerTest { get; }

        protected Mock<IRepositoryTest> MockRepositoryTest { get; set; }

        public TestsManagerTest()
        {
            MockRepositoryTest = new Mock<IRepositoryTest>();

            ManagerTest = new ManagerTest(
                Mapper,
                LoggerMock.Object,
                MockRepositoryTest.Object);
        }

        [Fact]
        public void GetTestById_Found()
        {
            MockRepositoryTest
                .Setup(x => x.GetTetsById(999))
                .Returns(new DatabaseTest { Subject = "Subject" });

            var test = ManagerTest.GetTestById(999, new OptionsTest());

            Assert.Equal("Subject", test.Subject);
        }

        [Fact]
        public void GetTetsById_NotFound()
        {
            MockRepositoryTest
                .Setup(x => x.GetTetsById(999))
                .Returns((DatabaseTest) null);

            Assert.Throws<EducationSystemNotFoundException>(
                () => ManagerTest.GetTestById(999, new OptionsTest()));
        }
    }
}