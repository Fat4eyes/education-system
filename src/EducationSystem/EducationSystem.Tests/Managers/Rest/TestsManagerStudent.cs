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
    public class TestsManagerStudent : TestsManager<ManagerStudent>
    {
        private readonly IManagerStudent _managerStudent;

        private readonly Mock<IRepositoryStudent> _mockRepositoryStudent
            = new Mock<IRepositoryStudent>();

        public TestsManagerStudent()
        {
            _managerStudent = new ManagerStudent(
                Mapper,
                LoggerMock.Object,
                MockHelperUser.Object,
                _mockRepositoryStudent.Object);
        }

        [Fact]
        public void GetStudentById_NotStudent()
        {
            MockHelperUser
                .Setup(x => x.CheckRoleStudent(999))
                .Throws<EducationSystemException>();

            Assert.Throws<EducationSystemException>(
                () => _managerStudent.GetStudentById(999, new OptionsStudent()));
        }

        [Fact]
        public void GetStudentById_Found()
        {
            MockHelperUser.Reset();

            _mockRepositoryStudent
                .Setup(x => x.GetById(999))
                .Returns(new DatabaseUser { FirstName = "Victor" });

            var student = _managerStudent.GetStudentById(999, new OptionsStudent());

            Assert.Equal("Victor", student.FirstName);
        }

        [Fact]
        public void GetStudentById_NotFound()
        {
            MockHelperUser.Reset();

            _mockRepositoryStudent
                .Setup(x => x.GetById(999))
                .Returns((DatabaseUser) null);

            Assert.Throws<EducationSystemNotFoundException>(
                () => _managerStudent.GetStudentById(999, new OptionsStudent()));
        }
    }
}