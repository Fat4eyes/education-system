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
    public class TestsManagerUser : TestsManager
    {
        protected IManagerUser ManagerUser { get; }

        protected Mock<IRepositoryUser> MockRepositoryUser { get; set; }

        public TestsManagerUser()
        {
            MockRepositoryUser = new Mock<IRepositoryUser>();

            ManagerUser = new ManagerUser(Mapper, MockRepositoryUser.Object);
        }

        [Fact]
        public void GetAll()
        {
            MockRepositoryUser
                .Setup(x => x.GetAll())
                .Returns(new List<DatabaseUser> {
                    new DatabaseUser { Id = 3, FirstName = "Дмитрий" },
                    new DatabaseUser { Id = 5, FirstName = "Виктор" }
                });

            var users = ManagerUser.GetAll();

            Assert.Equal("Виктор", users.Last().FirstName);
            Assert.Equal("Дмитрий", users.First().FirstName);
        }

        [Fact]
        public void GetById_User_Exists()
        {
            MockRepositoryUser
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(new DatabaseUser { Id = 5, FirstName = "Виктор" });

            var user = ManagerUser.GetById(5);

            Assert.Equal("Виктор", user.FirstName);
        }

        [Fact]
        public void GetById_User_Not_Exists()
        {
            MockRepositoryUser
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns((DatabaseUser) null);

            Assert.Throws<EducationSystemException>(() => ManagerUser.GetById(9));
        }
    }
}