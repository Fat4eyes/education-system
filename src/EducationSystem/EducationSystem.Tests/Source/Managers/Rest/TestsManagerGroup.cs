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
    public class TestsManagerGroup : TestsManager<ManagerGroup>
    {
        protected IManagerGroup ManagerGroup { get; }

        protected Mock<IRepositoryGroup> MockRepositoryGroup { get; set; }

        public TestsManagerGroup()
        {
            MockRepositoryGroup = new Mock<IRepositoryGroup>();

            ManagerGroup = new ManagerGroup(
                Mapper,
                LoggerMock.Object,
                MockHelperUser.Object,
                MockRepositoryGroup.Object);
        }

        [Fact]
        public void GetGroupById_Found()
        {
            MockRepositoryGroup
                .Setup(x => x.GetById(999))
                .Returns(new DatabaseGroup { Name = "ИС-42" });

            var group = ManagerGroup.GetGroupById(999, new OptionsGroup());

            Assert.Equal("ИС-42", group.Name);
        }

        [Fact]
        public void GetGroupById_NotFound()
        {
            MockRepositoryGroup
                .Setup(x => x.GetById(999))
                .Returns((DatabaseGroup) null);

            Assert.Throws<EducationSystemNotFoundException>(
                () => ManagerGroup.GetGroupById(999, new OptionsGroup()));
        }

        [Fact]
        public void GetGroupByStudentId_NotStudent()
        {
            MockHelperUser
                .Setup(x => x.CheckRoleStudent(999))
                .Throws<EducationSystemException>();

            Assert.Throws<EducationSystemException>(
                () => ManagerGroup.GetGroupByStudentId(999, new OptionsGroup()));
        }

        [Fact]
        public void GetGroupByStudentId_Found()
        {
            MockHelperUser.Reset();

            MockRepositoryGroup
                .Setup(x => x.GetGroupByStudentId(999))
                .Returns(new DatabaseGroup { Name = "ИС-42" });

            var group = ManagerGroup.GetGroupByStudentId(999, new OptionsGroup());

            Assert.Equal("ИС-42", group.Name);
        }

        [Fact]
        public void GetGroupByStudentId_NotFound()
        {
            MockHelperUser.Reset();

            MockRepositoryGroup
                .Setup(x => x.GetGroupByStudentId(999))
                .Returns((DatabaseGroup) null);

            Assert.Throws<EducationSystemNotFoundException>(
                () => ManagerGroup.GetGroupByStudentId(999, new OptionsGroup()));
        }
    }
}