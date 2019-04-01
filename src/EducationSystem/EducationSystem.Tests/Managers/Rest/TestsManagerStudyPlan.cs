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
        private readonly IManagerStudyPlan _managerStudyPlan;

        private readonly Mock<IRepositoryStudyPlan> _mockRepositoryStudyPlan
            = new Mock<IRepositoryStudyPlan>();

        public TestsManagerStudyPlan()
        {
            _managerStudyPlan = new ManagerStudyPlan(
                Mapper,
                LoggerMock.Object,
                MockHelperUser.Object,
                _mockRepositoryStudyPlan.Object);
        }

        [Fact]
        public void GetStudyPlanByStudentId_NotStudent()
        {
            MockHelperUser
                .Setup(x => x.CheckRoleStudent(999))
                .Throws<EducationSystemException>();

            Assert.Throws<EducationSystemException>(
                () => _managerStudyPlan.GetStudyPlanByStudentId(999, new OptionsStudyPlan()));
        }

        [Fact]
        public void GetStudyPlanByStudentId_Found()
        {
            MockHelperUser.Reset();

            _mockRepositoryStudyPlan
                .Setup(x => x.GetStudyPlanByStudentId(999))
                .Returns(new DatabaseStudyPlan { Name = "Study Plan" });

            var studyPlan = _managerStudyPlan.GetStudyPlanByStudentId(999, new OptionsStudyPlan());

            Assert.Equal("Study Plan", studyPlan.Name);
        }

        [Fact]
        public void GetStudyPlanByStudentId_NotFound()
        {
            MockHelperUser.Reset();

            _mockRepositoryStudyPlan
                .Setup(x => x.GetStudyPlanByStudentId(999))
                .Returns((DatabaseStudyPlan) null);

            Assert.Throws<EducationSystemNotFoundException>(
                () => _managerStudyPlan.GetStudyPlanByStudentId(999, new OptionsStudyPlan()));
        }
    }
}