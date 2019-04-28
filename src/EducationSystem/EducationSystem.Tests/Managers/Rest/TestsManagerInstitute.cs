using System.Threading.Tasks;
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
        public async Task GetInstituteByStudentId_NotStudent()
        {
            MockHelperUser
                .Setup(x => x.CheckRoleStudent(999))
                .Throws<EducationSystemException>();

            await Assert.ThrowsAsync<EducationSystemException>(
                () => _managerInstitute.GetInstituteByStudentId(999, new OptionsInstitute()));
        }

        [Fact]
        public async Task GetInstituteByStudentId_Found()
        {
            MockHelperUser.Reset();

            _mockRepositoryInstitute
                .Setup(x => x.GetInstituteByStudentId(999))
                .Returns(new DatabaseInstitute { Name = "ИИТиУвТС" });

            var institute = await _managerInstitute.GetInstituteByStudentId(999, new OptionsInstitute());

            Assert.Equal("ИИТиУвТС", institute.Name);
        }

        [Fact]
        public async Task GetInstituteByStudentId_NotFound()
        {
            MockHelperUser.Reset();

            _mockRepositoryInstitute
                .Setup(x => x.GetInstituteByStudentId(999))
                .Returns((DatabaseInstitute) null);

            await Assert.ThrowsAsync<EducationSystemNotFoundException>(
                () => _managerInstitute.GetInstituteByStudentId(999, new OptionsInstitute()));
        }
    }
}