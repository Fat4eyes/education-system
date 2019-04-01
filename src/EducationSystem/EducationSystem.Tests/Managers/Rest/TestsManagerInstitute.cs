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
    public class TestsManagerInstitute : TestsManager<ManagerInstitute>
    {
        private readonly IManagerInstitute _managerInstitute;

        private readonly Mock<IRepositoryInstitute> _mockRepositoryInstitute
            = new Mock<IRepositoryInstitute>();

        public TestsManagerInstitute()
        {
            _managerInstitute = new ManagerInstitute(
                Mapper,
                LoggerMock.Object,
                MockHelperUser.Object,
                _mockRepositoryInstitute.Object);
        }

        [Fact]
        public void GetInstituteByStudentId_NotStudent()
        {
            MockHelperUser
                .Setup(x => x.CheckRoleStudent(999))
                .Throws<EducationSystemException>();

            Assert.Throws<EducationSystemException>(
                () => _managerInstitute.GetInstituteByStudentId(999, new OptionsInstitute()));
        }

        [Fact]
        public void GetInstituteByStudentId_Found()
        {
            MockHelperUser.Reset();

            _mockRepositoryInstitute
                .Setup(x => x.GetInstituteByStudentId(999))
                .Returns(new DatabaseInstitute { Name = "ИИТиУвТС" });

            var institute = _managerInstitute.GetInstituteByStudentId(999, new OptionsInstitute());

            Assert.Equal("ИИТиУвТС", institute.Name);
        }

        [Fact]
        public void GetInstituteByStudentId_NotFound()
        {
            MockHelperUser.Reset();

            _mockRepositoryInstitute
                .Setup(x => x.GetInstituteByStudentId(999))
                .Returns((DatabaseInstitute) null);

            Assert.Throws<EducationSystemNotFoundException>(
                () => _managerInstitute.GetInstituteByStudentId(999, new OptionsInstitute()));
        }
    }
}