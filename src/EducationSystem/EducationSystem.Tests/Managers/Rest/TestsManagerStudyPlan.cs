using System.Threading.Tasks;
using EducationSystem.Database.Models;
using EducationSystem.Exceptions;
using EducationSystem.Implementations.Managers;
using EducationSystem.Interfaces.Managers;
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
        public async Task GetStudyPlanByStudentId_NotStudent()
        {
            MockHelperUser
                .Setup(x => x.CheckRoleStudent(999))
                .Throws<EducationSystemException>();

            await Assert.ThrowsAsync<EducationSystemException>(
                () => _managerStudyPlan.GetStudyPlanByStudentId(999, new OptionsStudyPlan()));
        }

        [Fact]
        public async Task GetStudyPlanByStudentId_Found()
        {
            MockHelperUser.Reset();

            _mockRepositoryStudyPlan
                .Setup(x => x.GetStudyPlanByStudentId(999))
                .Returns(new DatabaseStudyPlan { Name = "Study Plan" });

            var studyPlan = await _managerStudyPlan.GetStudyPlanByStudentId(999, new OptionsStudyPlan());

            Assert.Equal("Study Plan", studyPlan.Name);
        }

        [Fact]
        public async Task GetStudyPlanByStudentId_NotFound()
        {
            MockHelperUser.Reset();

            _mockRepositoryStudyPlan
                .Setup(x => x.GetStudyPlanByStudentId(999))
                .Returns((DatabaseStudyPlan) null);

            await Assert.ThrowsAsync<EducationSystemNotFoundException>(
                () => _managerStudyPlan.GetStudyPlanByStudentId(999, new OptionsStudyPlan()));
        }
    }
}