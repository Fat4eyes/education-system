using EducationSystem.Database.Models.Source;
using EducationSystem.Exceptions.Source;
using EducationSystem.Implementations.Managers.Rest;
using EducationSystem.Interfaces.Managers.Rest;
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

        public TestsManagerStudent()
        {
            MockRepositoryStudent = new Mock<IRepositoryStudent>();

            ManagerStudent = new ManagerStudent(
                Mapper,
                LoggerMock.Object,
                MockHelperUser.Object,
                MockRepositoryStudent.Object);
        }

        [Fact]
        public void GetStudentById_NotStudent()
        {
            MockHelperUser
                .Setup(x => x.CheckRoleStudent(999))
                .Throws<EducationSystemException>();

            Assert.Throws<EducationSystemException>(
                () => ManagerStudent.GetStudentById(999, new OptionsStudent()));
        }

        [Fact]
        public void GetStudentById_Found()
        {
            MockHelperUser.Reset();

            MockRepositoryStudent
                .Setup(x => x.GetById(999))
                .Returns(new DatabaseUser { FirstName = "Victor" });

            var student = ManagerStudent.GetStudentById(999, new OptionsStudent());

            Assert.Equal("Victor", student.FirstName);
        }

        [Fact]
        public void GetStudentById_NotFound()
        {
            MockHelperUser.Reset();

            MockRepositoryStudent
                .Setup(x => x.GetById(999))
                .Returns((DatabaseUser) null);

            Assert.Throws<EducationSystemNotFoundException>(
                () => ManagerStudent.GetStudentById(999, new OptionsStudent()));
        }
    }
}