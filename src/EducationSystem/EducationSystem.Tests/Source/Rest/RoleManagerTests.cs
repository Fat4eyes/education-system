using System.Collections.Generic;
using System.Linq;
using EducationSystem.Database.Models.Source;
using EducationSystem.Exceptions.Source;
using EducationSystem.Managers.Implementations.Source.Rest;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using EducationSystem.Tests.Source.Rest.Base;
using Moq;
using Xunit;

namespace EducationSystem.Tests.Source.Rest
{
    public class RoleManagerTests : BaseManagerTests<RoleManager>
    {
        protected IRoleManager RoleManager { get; }

        protected Mock<IRoleRepository> MockRepositoryRole { get; }

        public RoleManagerTests()
        {
            MockRepositoryRole = new Mock<IRoleRepository>();

            RoleManager = new RoleManager(
                Mapper,
                LoggerMock.Object,
                MockRepositoryRole.Object);
        }

        [Fact]
        public void GetAll()
        {
            MockRepositoryRole
                .Setup(x => x.GetAll())
                .Returns(new List<DatabaseRole> {
                    new DatabaseRole { Id = 3, Name = "Студент" },
                    new DatabaseRole { Id = 5, Name = "Администратор" }
                });

            var roles = RoleManager.GetAll();

            Assert.Equal("Администратор", roles.Last().Name);
            Assert.Equal("Студент", roles.First().Name);
        }

        [Fact]
        public void GetById_GroupExists()
        {
            MockRepositoryRole
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(new DatabaseRole { Id = 3, Name = "Студент" });

            var role = RoleManager.GetById(5);

            Assert.Equal("Студент", role.Name);
        }

        [Fact]
        public void GetById_GroupNotExists()
        {
            MockRepositoryRole
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns((DatabaseRole) null);

            Assert.Throws<EducationSystemNotFoundException>(() => RoleManager.GetById(9));
        }
    }
}