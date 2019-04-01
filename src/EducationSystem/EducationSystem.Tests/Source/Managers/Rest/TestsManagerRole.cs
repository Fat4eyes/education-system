using EducationSystem.Database.Models.Source;
using EducationSystem.Exceptions.Source;
using EducationSystem.Implementations.Managers.Rest;
using EducationSystem.Interfaces.Managers.Rest;
using EducationSystem.Models.Source.Options;
using EducationSystem.Repositories.Interfaces;
using Moq;
using Xunit;

namespace EducationSystem.Tests.Source.Managers.Rest
{
    public class TestsManagerRole : TestsManager<ManagerRole>
    {
        protected IManagerRole ManagerRole { get; }

        protected Mock<IRepositoryRole> MockRepositoryRole { get; set; }

        public TestsManagerRole()
        {
            MockRepositoryRole = new Mock<IRepositoryRole>();

            ManagerRole = new ManagerRole(
                Mapper,
                LoggerMock.Object,
                MockRepositoryRole.Object);
        }

        [Fact]
        public void GetRoleById_Found()
        {
            MockRepositoryRole
                .Setup(x => x.GetById(999))
                .Returns(new DatabaseRole { Name = "Student" });

            var role = ManagerRole.GetRoleById(999, new OptionsRole());

            Assert.Equal("Student", role.Name);
        }

        [Fact]
        public void GetRoleById_NotFound()
        {
            MockRepositoryRole
                .Setup(x => x.GetById(999))
                .Returns((DatabaseRole) null);

            Assert.Throws<EducationSystemNotFoundException>(
                () => ManagerRole.GetRoleById(999, new OptionsRole()));
        }

        [Fact]
        public void GetRoleByUserId_Found()
        {
            MockRepositoryRole
                .Setup(x => x.GetRoleByUserId(999))
                .Returns(new DatabaseRole { Name = "Student" });

            var role = ManagerRole.GetRoleByUserId(999, new OptionsRole());

            Assert.Equal("Student", role.Name);
        }

        [Fact]
        public void GetRoleByUserId_NotFound()
        {
            MockRepositoryRole
                .Setup(x => x.GetRoleByUserId(999))
                .Returns((DatabaseRole) null);

            Assert.Throws<EducationSystemNotFoundException>(
                () => ManagerRole.GetRoleByUserId(999, new OptionsRole()));
        }
    }
}