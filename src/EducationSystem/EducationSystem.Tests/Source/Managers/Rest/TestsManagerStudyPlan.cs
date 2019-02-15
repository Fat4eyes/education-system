using EducationSystem.Database.Models.Source;
using EducationSystem.Exceptions.Source;
using EducationSystem.Helpers.Interfaces.Source;
using EducationSystem.Managers.Implementations.Source.Rest;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source.Options;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Moq;
using Xunit;

namespace EducationSystem.Tests.Source.Managers.Rest
{
    public class TestsManagerStudyPlan : TestsManager<ManagerStudyPlan>
    {
        protected IManagerStudyPlan ManagerStudyPlan { get; }

        protected Mock<IRepositoryStudyPlan> MockRepositoryStudyPlan { get; set; }

        protected Mock<IUserHelper> MockUserHelper { get; set; }

        public TestsManagerStudyPlan()
        {
            MockRepositoryStudyPlan = new Mock<IRepositoryStudyPlan>();

            MockUserHelper = new Mock<IUserHelper>();

            ManagerStudyPlan = new ManagerStudyPlan(
                Mapper,
                LoggerMock.Object,
                MockUserHelper.Object,
                MockRepositoryStudyPlan.Object);
        }

        [Fact]
        public void GetStudyPlanByStudentId_NotStudent()
        {
            MockUserHelper
                .Setup(x => x.IsStudent(999))
                .Returns(false);

            Assert.Throws<EducationSystemException>(
                () => ManagerStudyPlan.GetStudyPlanByStudentId(999, new OptionsStudyPlan()));
        }

        [Fact]
        public void GetStudyPlanByStudentId_Found()
        {
            MockUserHelper
                .Setup(x => x.IsStudent(999))
                .Returns(true);

            MockRepositoryStudyPlan
                .Setup(x => x.GetStudyPlanByStudentId(999, It.IsAny<OptionsStudyPlan>()))
                .Returns(new DatabaseStudyPlan { Name = "Study Plan" });

            var studyPlan = ManagerStudyPlan.GetStudyPlanByStudentId(999, new OptionsStudyPlan());

            Assert.Equal("Study Plan", studyPlan.Name);
        }

        [Fact]
        public void GetStudyPlanByStudentId_NotFound()
        {
            MockUserHelper
                .Setup(x => x.IsStudent(999))
                .Returns(true);

            MockRepositoryStudyPlan
                .Setup(x => x.GetStudyPlanByStudentId(999, It.IsAny<OptionsStudyPlan>()))
                .Returns((DatabaseStudyPlan) null);

            Assert.Throws<EducationSystemNotFoundException>(
                () => ManagerStudyPlan.GetStudyPlanByStudentId(999, new OptionsStudyPlan()));
        }
    }
}