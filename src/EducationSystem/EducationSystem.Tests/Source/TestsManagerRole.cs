using Moq;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using EducationSystem.Database.Models.Source;
using EducationSystem.Exceptions.Source;
using EducationSystem.Managers.Implementations.Source.Rest;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Repositories.Interfaces.Source.Rest;

namespace EducationSystem.Tests.Source
{
    public class TestsManagerRole : TestsManager<ManagerRole>
    {
        protected IManagerRole ManagerGroup { get; }

        protected Mock<IRepositoryRole> MockRepositoryRole { get; }

        public TestsManagerRole()
        {
            MockRepositoryRole = new Mock<IRepositoryRole>();

            ManagerGroup = new ManagerRole(
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

            var groups = ManagerGroup.GetAll();

            Assert.Equal("Администратор", groups.Last().Name);
            Assert.Equal("Студент", groups.First().Name);
        }

        [Fact]
        public void GetById_GroupExists()
        {
            MockRepositoryRole
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(new DatabaseRole { Id = 3, Name = "Студент" });

            var role = ManagerGroup.GetById(5);

            Assert.Equal("Студент", role.Name);
        }

        [Fact]
        public void GetById_GroupNotExists()
        {
            MockRepositoryRole
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns((DatabaseRole) null);

            Assert.Throws<EducationSystemNotFoundException>(() => ManagerGroup.GetById(9));
        }
    }
}