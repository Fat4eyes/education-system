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
    public class TestsManagerUser : TestsManager<ManagerUser>
    {
        protected IManagerUser ManagerUser { get; }

        protected Mock<IRepositoryUser> MockRepositoryUser { get; set; }

        public TestsManagerUser()
        {
            MockRepositoryUser = new Mock<IRepositoryUser>();

            ManagerUser = new ManagerUser(
                Mapper,
                LoggerMock.Object,
                MockRepositoryUser.Object);
        }

        [Fact]
        public void GetUserById_Found()
        {
            MockRepositoryUser
                .Setup(x => x.GetUserById(999, It.IsAny<OptionsUser>()))
                .Returns(new DatabaseUser { FirstName = "Victor" });

            var user = ManagerUser.GetUserById(999, new OptionsUser());

            Assert.Equal("Victor", user.FirstName);
        }

        [Fact]
        public void GetUserById_NotFound()
        {
            MockRepositoryUser
                .Setup(x => x.GetUserById(999, It.IsAny<OptionsUser>()))
                .Returns((DatabaseUser) null);

            Assert.Throws<EducationSystemNotFoundException>(
                () => ManagerUser.GetUserById(999, new OptionsUser()));
        }
    }
}