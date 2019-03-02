﻿using EducationSystem.Database.Models.Source;
using EducationSystem.Exceptions.Source;
using EducationSystem.Managers.Implementations.Source.Rest;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source.Options;
using EducationSystem.Repositories.Interfaces.Source.Rest;
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
                MockUserHelper.Object,
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
            MockUserHelper
                .Setup(x => x.CheckRoleStudent(999))
                .Throws<EducationSystemException>();

            Assert.Throws<EducationSystemException>(
                () => ManagerGroup.GetGroupByStudentId(999, new OptionsGroup()));
        }

        [Fact]
        public void GetGroupByStudentId_Found()
        {
            MockUserHelper.Reset();

            MockRepositoryGroup
                .Setup(x => x.GetGroupByStudentId(999))
                .Returns(new DatabaseGroup { Name = "ИС-42" });

            var group = ManagerGroup.GetGroupByStudentId(999, new OptionsGroup());

            Assert.Equal("ИС-42", group.Name);
        }

        [Fact]
        public void GetGroupByStudentId_NotFound()
        {
            MockUserHelper.Reset();

            MockRepositoryGroup
                .Setup(x => x.GetGroupByStudentId(999))
                .Returns((DatabaseGroup) null);

            Assert.Throws<EducationSystemNotFoundException>(
                () => ManagerGroup.GetGroupByStudentId(999, new OptionsGroup()));
        }
    }
}