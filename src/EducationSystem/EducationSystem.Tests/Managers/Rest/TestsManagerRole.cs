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
    public class TestsManagerRole : TestsManager<ManagerRole>
    {
        private readonly IManagerRole _managerRole;

        private readonly Mock<IRepositoryRole> _mockRepositoryRole
            = new Mock<IRepositoryRole>();

        public TestsManagerRole()
        {
            _managerRole = new ManagerRole(
                Mapper,
                LoggerMock.Object,
                _mockRepositoryRole.Object);
        }

        [Fact]
        public void GetRoleById_Found()
        {
            _mockRepositoryRole
                .Setup(x => x.GetById(999))
                .Returns(new DatabaseRole { Name = "Student" });

            var role = _managerRole.GetRoleById(999, new OptionsRole());

            Assert.Equal("Student", role.Name);
        }

        [Fact]
        public void GetRoleById_NotFound()
        {
            _mockRepositoryRole
                .Setup(x => x.GetById(999))
                .Returns((DatabaseRole) null);

            Assert.Throws<EducationSystemNotFoundException>(
                () => _managerRole.GetRoleById(999, new OptionsRole()));
        }

        [Fact]
        public void GetRoleByUserId_Found()
        {
            _mockRepositoryRole
                .Setup(x => x.GetRoleByUserId(999))
                .Returns(new DatabaseRole { Name = "Student" });

            var role = _managerRole.GetRoleByUserId(999, new OptionsRole());

            Assert.Equal("Student", role.Name);
        }

        [Fact]
        public void GetRoleByUserId_NotFound()
        {
            _mockRepositoryRole
                .Setup(x => x.GetRoleByUserId(999))
                .Returns((DatabaseRole) null);

            Assert.Throws<EducationSystemNotFoundException>(
                () => _managerRole.GetRoleByUserId(999, new OptionsRole()));
        }
    }
}