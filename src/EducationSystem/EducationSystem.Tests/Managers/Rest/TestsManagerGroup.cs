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
    public class TestsManagerGroup : TestsManager<ManagerGroup>
    {
        private readonly IManagerGroup _managerGroup;

        private readonly Mock<IRepositoryGroup> _mockRepositoryGroup
            = new Mock<IRepositoryGroup>();

        public TestsManagerGroup()
        {
            _managerGroup = new ManagerGroup(
                Mapper,
                LoggerMock.Object,
                MockHelperUser.Object,
                _mockRepositoryGroup.Object);
        }

        [Fact]
        public void GetGroupById_Found()
        {
            _mockRepositoryGroup
                .Setup(x => x.GetById(999))
                .Returns(new DatabaseGroup { Name = "ИС-42" });

            var group = _managerGroup.GetGroupById(999, new OptionsGroup());

            Assert.Equal("ИС-42", group.Name);
        }

        [Fact]
        public void GetGroupById_NotFound()
        {
            _mockRepositoryGroup
                .Setup(x => x.GetById(999))
                .Returns((DatabaseGroup) null);

            Assert.Throws<EducationSystemNotFoundException>(
                () => _managerGroup.GetGroupById(999, new OptionsGroup()));
        }

        [Fact]
        public void GetGroupByStudentId_NotStudent()
        {
            MockHelperUser
                .Setup(x => x.CheckRoleStudent(999))
                .Throws<EducationSystemException>();

            Assert.Throws<EducationSystemException>(
                () => _managerGroup.GetGroupByStudentId(999, new OptionsGroup()));
        }

        [Fact]
        public void GetGroupByStudentId_Found()
        {
            MockHelperUser.Reset();

            _mockRepositoryGroup
                .Setup(x => x.GetGroupByStudentId(999))
                .Returns(new DatabaseGroup { Name = "ИС-42" });

            var group = _managerGroup.GetGroupByStudentId(999, new OptionsGroup());

            Assert.Equal("ИС-42", group.Name);
        }

        [Fact]
        public void GetGroupByStudentId_NotFound()
        {
            MockHelperUser.Reset();

            _mockRepositoryGroup
                .Setup(x => x.GetGroupByStudentId(999))
                .Returns((DatabaseGroup) null);

            Assert.Throws<EducationSystemNotFoundException>(
                () => _managerGroup.GetGroupByStudentId(999, new OptionsGroup()));
        }
    }
}