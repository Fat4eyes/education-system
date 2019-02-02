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
    public class StudyProfileManagerTests : BaseManagerTests<StudyProfileManager>
    {
        protected IStudyProfileManager StudyProfileManager { get; }

        protected Mock<IStudyProfileRepository> MockRepositoryStudyProfile { get; }

        public StudyProfileManagerTests()
        {
            MockRepositoryStudyProfile = new Mock<IStudyProfileRepository>();

            StudyProfileManager = new StudyProfileManager(
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

            var users = StudyProfileManager.GetAll();

            Assert.Equal("Профиль Обучения Б", users.Last().Name);
            Assert.Equal("Профиль Обучения А", users.First().Name);
        }

        [Fact]
        public void GetById_StudyProfileExists()
        {
            MockRepositoryStudyProfile
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(new DatabaseStudyProfile { Id = 3, Name = "Профиль Обучения А" });

            var user = StudyProfileManager.GetById(5);

            Assert.Equal("Профиль Обучения А", user.Name);
        }

        [Fact]
        public void GetById_StudyProfileNotExists()
        {
            MockRepositoryStudyProfile
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns((DatabaseStudyProfile) null);

            Assert.Throws<EducationSystemNotFoundException>(() => StudyProfileManager.GetById(9));
        }
    }
}