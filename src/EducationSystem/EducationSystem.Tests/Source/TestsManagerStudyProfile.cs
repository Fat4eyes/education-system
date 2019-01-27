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
    public class TestsManagerStudyProfile : TestsManager<ManagerStudyProfile>
    {
        protected IManagerStudyProfile ManagerStudyProfile { get; }

        protected Mock<IRepositoryStudyProfile> MockRepositoryStudyProfile { get; }

        public TestsManagerStudyProfile()
        {
            MockRepositoryStudyProfile = new Mock<IRepositoryStudyProfile>();

            ManagerStudyProfile = new ManagerStudyProfile(
                Mapper,
                LoggerMock.Object,
                MockRepositoryStudyProfile.Object);
        }

        [Fact]
        public void GetAll()
        {
            MockRepositoryStudyProfile
                .Setup(x => x.GetAll())
                .Returns(new List<DatabaseStudyProfile> {
                    new DatabaseStudyProfile { Id = 3, Name = "Профиль Обучения А" },
                    new DatabaseStudyProfile { Id = 5, Name = "Профиль Обучения Б" }
                });

            var users = ManagerStudyProfile.GetAll();

            Assert.Equal("Профиль Обучения Б", users.Last().Name);
            Assert.Equal("Профиль Обучения А", users.First().Name);
        }

        [Fact]
        public void GetById_StudyProfileExists()
        {
            MockRepositoryStudyProfile
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(new DatabaseStudyProfile { Id = 3, Name = "Профиль Обучения А" });

            var user = ManagerStudyProfile.GetById(5);

            Assert.Equal("Профиль Обучения А", user.Name);
        }

        [Fact]
        public void GetById_StudyProfileNotExists()
        {
            MockRepositoryStudyProfile
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns((DatabaseStudyProfile) null);

            Assert.Throws<EducationSystemNotFoundException>(() => ManagerStudyProfile.GetById(9));
        }
    }
}