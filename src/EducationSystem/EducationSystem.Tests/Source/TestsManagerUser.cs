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
    public class TestsManagerUser : TestsManager<ManagerUser>
    {
        protected IManagerUser ManagerUser { get; }

        protected Mock<IRepositoryUser> MockRepositoryUser { get; }

        public TestsManagerUser()
        {
            MockRepositoryUser = new Mock<IRepositoryUser>();

            ManagerUser = new ManagerUser(
                Mapper,
                LoggerMock.Object,
                MockRepositoryUser.Object);
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
        public void GetById_UserExists()
        {
            MockRepositoryUser
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(new DatabaseUser { Id = 5, FirstName = "Виктор" });

            var user = ManagerUser.GetById(5);

            Assert.Equal("Виктор", user.FirstName);
        }

        [Fact]
        public void GetById_UserNotExists()
        {
            MockRepositoryUser
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns((DatabaseUser) null);

            Assert.Throws<EducationSystemNotFoundException>(() => ManagerUser.GetById(9));
        }

        [Fact]
        public void GetByEmailAndPassword_UserExists_PasswordValid()
        {
            MockRepositoryUser
                .Setup(x => x.GetByEmail(It.IsAny<string>()))
                .Returns(new DatabaseUser {
                    FirstName = "Виктор",
                    Email = "email@gmail.com",
                    Password = "$2y$10$MbINClu9lHiaxS.TNNNHPeDJGdNJws2LwNrYqu06HNgy9a9hQ9fau"
                });

            var user = ManagerUser.GetByEmailAndPassword("EMAIL@gmail.com", "qwerty");

            Assert.Equal("Виктор", user.FirstName);
            Assert.Equal("email@gmail.com", user.Email);
        }

        [Fact]
        public void GetByEmailAndPassword_UserNotExists()
        {
            MockRepositoryUser
                .Setup(x => x.GetByEmail(It.IsAny<string>()))
                .Returns((DatabaseUser) null);

            Assert.Throws<EducationSystemNotFoundException>(() =>
                ManagerUser.GetByEmailAndPassword("email@gmail.com", "qwerty"));
        }

        [Fact]
        public void GetByEmailAndPassword_UserExists_PasswordInvalid()
        {
            MockRepositoryUser
                .Setup(x => x.GetByEmail(It.IsAny<string>()))
                .Returns(new DatabaseUser {
                    FirstName = "Виктор",
                    Email = "email@gmail.com",
                    Password = "$2y$10$zLMh5ShFAL8n2UcUo0HfPOzbyvybnQ4.ow6JxBABrIuEau/KpJSt6"
                });

            Assert.Throws<EducationSystemException>(() =>
                ManagerUser.GetByEmailAndPassword("email@gmail.com", "qwerty-m"));
        }
    }
}