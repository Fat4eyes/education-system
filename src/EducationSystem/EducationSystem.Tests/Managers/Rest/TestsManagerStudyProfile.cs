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
        public void GetStudyProfileByStudentId_NotStudent()
        {
            MockHelperUser
                .Setup(x => x.CheckRoleStudent(999))
                .Throws<EducationSystemException>();

            Assert.Throws<EducationSystemException>(
                () => _managerStudyProfile.GetStudyProfileByStudentId(999, new OptionsStudyProfile()));
        }

        [Fact]
        public void GetStudyProfileByStudentId_Found()
        {
            MockHelperUser.Reset();

            _mockRepositoryStudyProfile
                .Setup(x => x.GetStudyProfileByStudentId(999))
                .Returns(new DatabaseStudyProfile { Name = "Study Profile" });

            var studyProfile = _managerStudyProfile.GetStudyProfileByStudentId(999, new OptionsStudyProfile());

            Assert.Equal("Study Profile", studyProfile.Name);
        }

        [Fact]
        public void GetStudyProfileByStudentId_NotFound()
        {
            MockHelperUser.Reset();

            _mockRepositoryStudyProfile
                .Setup(x => x.GetStudyProfileByStudentId(999))
                .Returns((DatabaseStudyProfile) null);

            Assert.Throws<EducationSystemNotFoundException>(
                () => _managerStudyProfile.GetStudyProfileByStudentId(999, new OptionsStudyProfile()));
        }
    }
}