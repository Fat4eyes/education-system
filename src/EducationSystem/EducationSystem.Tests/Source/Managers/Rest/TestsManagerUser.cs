using EducationSystem.Database.Models;
using EducationSystem.Exceptions;
using EducationSystem.Implementations.Managers.Rest;
using EducationSystem.Interfaces.Managers.Rest;
using EducationSystem.Models.Options;
using EducationSystem.Repositories.Interfaces;
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
                .Setup(x => x.GetById(999))
                .Returns(new DatabaseUser { FirstName = "Victor" });

            var user = ManagerUser.GetUserById(999, new OptionsUser());

            Assert.Equal("Victor", user.FirstName);
        }

        [Fact]
        public void GetUserById_NotFound()
        {
            MockRepositoryUser
                .Setup(x => x.GetById(999))
                .Returns((DatabaseUser) null);

            Assert.Throws<EducationSystemNotFoundException>(
                () => ManagerUser.GetUserById(999, new OptionsUser()));
        }
    }
}