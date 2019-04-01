using EducationSystem.Database.Models;
using EducationSystem.Exceptions;
using EducationSystem.Implementations.Managers.Rest;
using EducationSystem.Interfaces.Managers.Rest;
using EducationSystem.Models.Options;
using EducationSystem.Repositories.Interfaces;
using Moq;
using Xunit;

namespace EducationSystem.Tests.Managers.Rest
{
    public class TestsManagerUser : TestsManager<ManagerUser>
    {
        private readonly IManagerUser _managerUser;

        private readonly Mock<IRepositoryUser> _mockRepositoryUser
            = new Mock<IRepositoryUser>();

        public TestsManagerUser()
        {
            _managerUser = new ManagerUser(
                Mapper,
                LoggerMock.Object,
                _mockRepositoryUser.Object);
        }

        [Fact]
        public void GetUserById_Found()
        {
            _mockRepositoryUser
                .Setup(x => x.GetById(999))
                .Returns(new DatabaseUser { FirstName = "Victor" });

            var user = _managerUser.GetUserById(999, new OptionsUser());

            Assert.Equal("Victor", user.FirstName);
        }

        [Fact]
        public void GetUserById_NotFound()
        {
            _mockRepositoryUser
                .Setup(x => x.GetById(999))
                .Returns((DatabaseUser) null);

            Assert.Throws<EducationSystemNotFoundException>(
                () => _managerUser.GetUserById(999, new OptionsUser()));
        }
    }
}