using EducationSystem.Database.Models.Source;
using EducationSystem.Exceptions.Source;
using EducationSystem.Managers.Implementations.Source.Rest;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source.Options;
using EducationSystem.Repositories.Interfaces.Source.Rest;
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
                MockUserHelper.Object,
                MockRepositoryStudyProfile.Object);
        }

        [Fact]
        public void GetStudyProfileByStudentId_NotStudent()
        {
            MockUserHelper
                .Setup(x => x.IsStudent(999))
                .Returns(false);

            Assert.Throws<EducationSystemException>(
                () => ManagerStudyProfile.GetStudyProfileByStudentId(999, new OptionsStudyProfile()));
        }

        [Fact]
        public void GetStudyProfileByStudentId_Found()
        {
            MockUserHelper
                .Setup(x => x.IsStudent(999))
                .Returns(true);

            MockRepositoryStudyProfile
                .Setup(x => x.GetStudyProfileByStudentId(999))
                .Returns(new DatabaseStudyProfile { Name = "Study Profile" });

            var studyProfile = ManagerStudyProfile.GetStudyProfileByStudentId(999, new OptionsStudyProfile());

            Assert.Equal("Study Profile", studyProfile.Name);
        }

        [Fact]
        public void GetStudyProfileByStudentId_NotFound()
        {
            MockUserHelper
                .Setup(x => x.IsStudent(999))
                .Returns(true);

            MockRepositoryStudyProfile
                .Setup(x => x.GetStudyProfileByStudentId(999))
                .Returns((DatabaseStudyProfile) null);

            Assert.Throws<EducationSystemNotFoundException>(
                () => ManagerStudyProfile.GetStudyProfileByStudentId(999, new OptionsStudyProfile()));
        }
    }
}