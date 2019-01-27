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
    public class TestsManagerGroup : TestsManager
    {
        protected IManagerGroup ManagerGroup { get; }

        protected Mock<IRepositoryGroup> MockRepositoryGroup { get; set; }

        public TestsManagerGroup()
        {
            MockRepositoryGroup = new Mock<IRepositoryGroup>();

            ManagerGroup = new ManagerGroup(Mapper, MockRepositoryGroup.Object);
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

            var users = ManagerGroup.GetAll();

            Assert.Equal("ИС-21о", users.Last().Name);
            Assert.Equal("ИС-42о", users.First().Name);
        }

        [Fact]
        public void GetById_Group_Exists()
        {
            MockRepositoryGroup
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(new DatabaseGroup { Id = 3, Name = "ИС-42о" });

            var user = ManagerGroup.GetById(5);

            Assert.Equal("ИС-42о", user.Name);
        }

        [Fact]
        public void GetById_Group_Not_Exists()
        {
            MockRepositoryGroup
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns((DatabaseGroup) null);

            Assert.Throws<EducationSystemNotFoundException>(() => ManagerGroup.GetById(9));
        }
    }
}