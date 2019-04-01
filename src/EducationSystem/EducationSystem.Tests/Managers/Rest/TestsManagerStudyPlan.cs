using EducationSystem.Database.Models;
using EducationSystem.Exceptions;
using EducationSystem.Implementations.Managers.Rest;
using EducationSystem.Interfaces.Managers.Rest;
using EducationSystem.Models.Options;
using EducationSystem.Repositories.Interfaces;
using Moq;
using Xunit;

namespace EducationSystem.Tests.Managers.Rest
{
    public class TestsManagerStudyPlan : TestsManager<ManagerStudyPlan>
    {
        protected IManagerStudyPlan ManagerStudyPlan { get; }

        protected Mock<IRepositoryStudyPlan> MockRepositoryStudyPlan { get; set; }

        public TestsManagerStudyPlan()
        {
            MockRepositoryStudyPlan = new Mock<IRepositoryStudyPlan>();

            ManagerStudyPlan = new ManagerStudyPlan(
                Mapper,
                LoggerMock.Object,
                MockHelperUser.Object,
                MockRepositoryStudyPlan.Object);
        }

        [Fact]
        public void GetStudyPlanByStudentId_NotStudent()
        {
            MockHelperUser
                .Setup(x => x.CheckRoleStudent(999))
                .Throws<EducationSystemException>();

            Assert.Throws<EducationSystemException>(
                () => ManagerStudyPlan.GetStudyPlanByStudentId(999, new OptionsStudyPlan()));
        }

        [Fact]
        public void GetStudyPlanByStudentId_Found()
        {
            MockHelperUser.Reset();

            MockRepositoryStudyPlan
                .Setup(x => x.GetStudyPlanByStudentId(999))
                .Returns(new DatabaseStudyPlan { Name = "Study Plan" });

            var studyPlan = ManagerStudyPlan.GetStudyPlanByStudentId(999, new OptionsStudyPlan());

            Assert.Equal("Study Plan", studyPlan.Name);
        }

        [Fact]
        public void GetStudyPlanByStudentId_NotFound()
        {
            MockHelperUser.Reset();

            MockRepositoryStudyPlan
                .Setup(x => x.GetStudyPlanByStudentId(999))
                .Returns((DatabaseStudyPlan) null);

            Assert.Throws<EducationSystemNotFoundException>(
                () => ManagerStudyPlan.GetStudyPlanByStudentId(999, new OptionsStudyPlan()));
        }
    }
}