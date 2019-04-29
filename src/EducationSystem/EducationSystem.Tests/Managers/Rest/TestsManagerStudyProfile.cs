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
    public class TestsManagerStudyProfile : TestsManager<ManagerStudyProfile>
    {
        private readonly IManagerStudyProfile _managerStudyProfile;

        private readonly Mock<IRepositoryStudyProfile> _mockRepositoryStudyProfile
            = new Mock<IRepositoryStudyProfile>();

        public TestsManagerStudyProfile()
        {
            _managerStudyProfile = new ManagerStudyProfile(
                Mapper,
                LoggerMock.Object,
                MockHelperUser.Object,
                _mockRepositoryStudyProfile.Object);
        }

        [Fact]
        public async Task GetStudyProfileByStudentId_NotStudent()
        {
            MockHelperUser
                .Setup(x => x.CheckRoleStudent(999))
                .Throws<EducationSystemException>();

            await Assert.ThrowsAsync<EducationSystemException>(
                () => _managerStudyProfile.GetStudyProfileByStudentId(999, new OptionsStudyProfile()));
        }

        [Fact]
        public async Task GetStudyProfileByStudentId_Found()
        {
            MockHelperUser.Reset();

            _mockRepositoryStudyProfile
                .Setup(x => x.GetStudyProfileByStudentId(999))
                .ReturnsAsync(new DatabaseStudyProfile { Name = "Study Profile" });

            var studyProfile = await _managerStudyProfile.GetStudyProfileByStudentId(999, new OptionsStudyProfile());

            Assert.Equal("Study Profile", studyProfile.Name);
        }

        [Fact]
        public async Task GetStudyProfileByStudentId_NotFound()
        {
            MockHelperUser.Reset();

            _mockRepositoryStudyProfile
                .Setup(x => x.GetStudyProfileByStudentId(999))
                .ReturnsAsync((DatabaseStudyProfile) null);

            await Assert.ThrowsAsync<EducationSystemNotFoundException>(
                () => _managerStudyProfile.GetStudyProfileByStudentId(999, new OptionsStudyProfile()));
        }
    }
}