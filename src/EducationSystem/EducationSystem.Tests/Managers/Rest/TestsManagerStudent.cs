using System.Threading.Tasks;
using EducationSystem.Database.Models;
using EducationSystem.Exceptions;
using EducationSystem.Implementations.Managers;
using EducationSystem.Interfaces.Managers;
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
        public async Task GetStudent_NotStudent()
        {
            MockHelperUser
                .Setup(x => x.CheckRoleStudent(999))
                .Throws<EducationSystemException>();

            await Assert.ThrowsAsync<EducationSystemException>(
                () => _managerStudent.GetStudent(999));
        }

        [Fact]
        public async Task GetStudent_Found()
        {
            MockHelperUser.Reset();

            _mockRepositoryStudent
                .Setup(x => x.GetById(999))
                .Returns(new DatabaseUser { FirstName = "Victor" });

            var student = await _managerStudent.GetStudent(999);

            Assert.Equal("Victor", student.FirstName);
        }

        [Fact]
        public async Task GetStudent_NotFound()
        {
            MockHelperUser.Reset();

            _mockRepositoryStudent
                .Setup(x => x.GetById(999))
                .Returns((DatabaseUser) null);

            await Assert.ThrowsAsync<EducationSystemNotFoundException>(
                () => _managerStudent.GetStudent(999));
        }
    }
}