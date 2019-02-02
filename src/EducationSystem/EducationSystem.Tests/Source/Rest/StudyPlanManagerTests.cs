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
    public class StudyPlanManagerTests : BaseManagerTests<StudyPlanManager>
    {
        protected IStudyPlanManager StudyPlanManager { get; }

        protected Mock<IStudyPlanRepository> MockRepositoryStudyPlan { get; }

        public StudyPlanManagerTests()
        {
            MockRepositoryStudyPlan = new Mock<IStudyPlanRepository>();

            StudyPlanManager = new StudyPlanManager(
                Mapper,
                LoggerMock.Object,
                MockRepositoryStudyPlan.Object);
        }

        [Fact]
        public void GetAll()
        {
            MockRepositoryStudyPlan
                .Setup(x => x.GetAll())
                .Returns(new List<DatabaseStudyPlan> {
                    new DatabaseStudyPlan { Id = 3, Name = "Учебный План А" },
                    new DatabaseStudyPlan { Id = 5, Name = "Учебный План Б" }
                });

            var users = StudyPlanManager.GetAll();

            Assert.Equal("Учебный План Б", users.Last().Name);
            Assert.Equal("Учебный План А", users.First().Name);
        }

        [Fact]
        public void GetById_StudyPlanExists()
        {
            MockRepositoryStudyPlan
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(new DatabaseStudyPlan { Id = 3, Name = "Учебный План А" });

            var user = StudyPlanManager.GetById(5);

            Assert.Equal("Учебный План А", user.Name);
        }

        [Fact]
        public void GetById_StudyPlanNotExists()
        {
            MockRepositoryStudyPlan
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns((DatabaseStudyPlan) null);

            Assert.Throws<EducationSystemNotFoundException>(() => StudyPlanManager.GetById(9));
        }
    }
}