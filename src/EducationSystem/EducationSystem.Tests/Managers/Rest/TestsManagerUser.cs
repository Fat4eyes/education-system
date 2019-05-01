using System.Threading.Tasks;
using EducationSystem.Database.Models;
using EducationSystem.Exceptions;
using EducationSystem.Implementations.Managers;
using EducationSystem.Interfaces.Managers;
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
        public async Task GetUser_Found()
        {
            _mockRepositoryUser
                .Setup(x => x.GetByIdAsync(999))
                .ReturnsAsync(new DatabaseUser { FirstName = "Victor" });

            var user = await _managerUser.GetUserAsync(999, new OptionsUser());

            Assert.Equal("Victor", user.FirstName);
        }

        [Fact]
        public async Task GetUser_NotFound()
        {
            _mockRepositoryUser
                .Setup(x => x.GetByIdAsync(999))
                .ReturnsAsync((DatabaseUser) null);

            await Assert.ThrowsAsync<EducationSystemNotFoundException>(
                () => _managerUser.GetUserAsync(999, new OptionsUser()));
        }
    }
}