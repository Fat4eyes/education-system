using Moq;
using Xunit;
using System.Linq;
using System.Collections.Generic;
using EducationSystem.Database.Models.Source;
using EducationSystem.Exceptions.Source;
using EducationSystem.Managers.Implementations.Source.Rest;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Repositories.Interfaces.Source.Rest;

namespace EducationSystem.Tests.Source
{
    public class TestsManagerGroup : TestsManager<ManagerGroup>
    {
        protected IManagerGroup ManagerGroup { get; }

        protected Mock<IRepositoryGroup> MockRepositoryGroup { get; }

        public TestsManagerGroup()
        {
            MockRepositoryGroup = new Mock<IRepositoryGroup>();

            ManagerGroup = new ManagerGroup(
                Mapper,
                LoggerMock.Object,
                MockRepositoryGroup.Object);
        }

        [Fact]
        public void GetAll()
        {
            MockRepositoryGroup
                .Setup(x => x.GetAll())
                .Returns(new List<DatabaseGroup> {
                    new DatabaseGroup { Id = 3, Name = "ИС-42о" },
                    new DatabaseGroup { Id = 5, Name = "ИС-21о" }
                });

            var groups = ManagerGroup.GetAll();

            Assert.Equal("ИС-21о", groups.Last().Name);
            Assert.Equal("ИС-42о", groups.First().Name);
        }

        [Fact]
        public void GetById_GroupExists()
        {
            MockRepositoryGroup
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(new DatabaseGroup { Id = 3, Name = "ИС-42о" });

            var group = ManagerGroup.GetById(5);

            Assert.Equal("ИС-42о", group.Name);
        }

        [Fact]
        public void GetById_GroupNotExists()
        {
            MockRepositoryGroup
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns((DatabaseGroup) null);

            Assert.Throws<EducationSystemNotFoundException>(() => ManagerGroup.GetById(9));
        }
    }
}