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
    public class TestsManagerStudent : TestsManager<ManagerStudent>
    {
        protected IManagerStudent ManagerStudent { get; }

        protected Mock<IRepositoryStudent> MockRepositoryStudent { get; set; }

        protected Mock<IUserHelper> MockUserHelper { get; set; }

        public TestsManagerStudent()
        {
            MockRepositoryStudent = new Mock<IRepositoryStudent>();

            MockUserHelper = new Mock<IUserHelper>();

            ManagerStudent = new ManagerStudent(
                Mapper,
                LoggerMock.Object,
                MockUserHelper.Object,
                MockRepositoryStudent.Object);
        }

        [Fact]
        public void GetStudentById_NotStudent()
        {
            MockUserHelper
                .Setup(x => x.IsStudent(999))
                .Returns(false);

            Assert.Throws<EducationSystemException>(
                () => ManagerStudent.GetStudentById(999, new OptionsStudent()));
        }

        [Fact]
        public void GetStudentById_Found()
        {
            MockUserHelper
                .Setup(x => x.IsStudent(999))
                .Returns(true);

            MockRepositoryStudent
                .Setup(x => x.GetStudentById(999, It.IsAny<OptionsStudent>()))
                .Returns(new DatabaseUser { FirstName = "Victor" });

            var student = ManagerStudent.GetStudentById(999, new OptionsStudent());

            Assert.Equal("Victor", student.FirstName);
        }

        [Fact]
        public void GetStudentById_NotFound()
        {
            MockUserHelper
                .Setup(x => x.IsStudent(999))
                .Returns(true);

            MockRepositoryStudent
                .Setup(x => x.GetStudentById(999, It.IsAny<OptionsStudent>()))
                .Returns((DatabaseUser) null);

            Assert.Throws<EducationSystemNotFoundException>(
                () => ManagerStudent.GetStudentById(999, new OptionsStudent()));
        }
    }
}