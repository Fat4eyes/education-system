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
    public class GroupManagerTests : BaseManagerTests<GroupManager>
    {
        protected IGroupManager GroupManager { get; }

        protected Mock<IGroupRepository> MockRepositoryGroup { get; }

        public GroupManagerTests()
        {
            MockRepositoryGroup = new Mock<IGroupRepository>();

            GroupManager = new GroupManager(
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

            var groups = GroupManager.GetAll();

            Assert.Equal("ИС-21о", groups.Last().Name);
            Assert.Equal("ИС-42о", groups.First().Name);
        }

        [Fact]
        public void GetById_GroupExists()
        {
            MockRepositoryGroup
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(new DatabaseGroup { Id = 3, Name = "ИС-42о" });

            var group = GroupManager.GetById(5);

            Assert.Equal("ИС-42о", group.Name);
        }

        [Fact]
        public void GetById_GroupNotExists()
        {
            MockRepositoryGroup
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns((DatabaseGroup) null);

            Assert.Throws<EducationSystemNotFoundException>(() => GroupManager.GetById(9));
        }
    }
}