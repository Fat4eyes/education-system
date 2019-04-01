using EducationSystem.Database.Models;
using EducationSystem.Exceptions;
using EducationSystem.Implementations.Managers.Rest;
using EducationSystem.Interfaces.Managers.Rest;
using EducationSystem.Models.Options;
using EducationSystem.Repositories.Interfaces;
using Moq;
using Xunit;

namespace EducationSystem.Tests.Source.Managers.Rest
{
    public class TestsManagerStudyProfile : TestsManager<ManagerStudyProfile>
    {
        protected IManagerStudyProfile ManagerStudyProfile { get; }

        protected Mock<IRepositoryStudyProfile> MockRepositoryStudyProfile { get; set; }

        public TestsManagerStudyProfile()
        {
            MockRepositoryStudyProfile = new Mock<IRepositoryStudyProfile>();

            ManagerStudyProfile = new ManagerStudyProfile(
                Mapper,
                LoggerMock.Object,
                MockHelperUser.Object,
                MockRepositoryStudyProfile.Object);
        }

        [Fact]
        public void GetStudyProfileByStudentId_NotStudent()
        {
            MockHelperUser
                .Setup(x => x.CheckRoleStudent(999))
                .Throws<EducationSystemException>();

            Assert.Throws<EducationSystemException>(
                () => ManagerStudyProfile.GetStudyProfileByStudentId(999, new OptionsStudyProfile()));
        }

        [Fact]
        public void GetStudyProfileByStudentId_Found()
        {
            MockHelperUser.Reset();

            MockRepositoryStudyProfile
                .Setup(x => x.GetStudyProfileByStudentId(999))
                .Returns(new DatabaseStudyProfile { Name = "Study Profile" });

            var studyProfile = ManagerStudyProfile.GetStudyProfileByStudentId(999, new OptionsStudyProfile());

            Assert.Equal("Study Profile", studyProfile.Name);
        }

        [Fact]
        public void GetStudyProfileByStudentId_NotFound()
        {
            MockHelperUser.Reset();

            MockRepositoryStudyProfile
                .Setup(x => x.GetStudyProfileByStudentId(999))
                .Returns((DatabaseStudyProfile) null);

            Assert.Throws<EducationSystemNotFoundException>(
                () => ManagerStudyProfile.GetStudyProfileByStudentId(999, new OptionsStudyProfile()));
        }
    }
}