using Moq;
using Xunit;
using EducationSystem.Database.Models.Source;
using EducationSystem.Exceptions.Source;
using EducationSystem.Managers.Implementations.Source.Rest;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Repositories.Interfaces.Source.Rest;

namespace EducationSystem.Tests.Source.Rest
{
    public class TestsManagerUser : TestsManager<ManagerUser>
    {
        protected IManagerUser ManagerUser { get; }

        protected Mock<IRepositoryUser> MockRepositoryUser { get; }

        public TestsManagerUser()
        {
            MockRepositoryUser = new Mock<IRepositoryUser>();

            ManagerUser = new ManagerUser(
                Mapper,
                LoggerMock.Object,
                MockRepositoryUser.Object);
        }

        [Fact]
        public void GetUserByEmail_UserExists()
        {
            MockRepositoryUser
                .Setup(x => x.GetByEmail(It.IsAny<string>()))
                .Returns(new DatabaseUser { Email = "duck@gmail.com", FirstName = "Виктор" });

            var user = ManagerUser.GetUserByEmail("duck@gmail.com");

            Assert.Equal("Виктор", user.FirstName);
        }

        [Fact]
        public void GetUserByEmail_UserNotExists()
        {
            MockRepositoryUser
                .Setup(x => x.GetByEmail(It.IsAny<string>()))
                .Returns((DatabaseUser) null);

            Assert.Throws<EducationSystemNotFoundException>(() =>
                ManagerUser.GetUserByEmail("duck@gmail.com"));
        }
    }
}