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
    public class TestsManagerInstitute : TestsManager<ManagerInstitute>
    {
        protected IManagerInstitute ManagerInstitute { get; }

        protected Mock<IRepositoryInstitute> MockRepositoryInstitute { get; set; }

        public TestsManagerInstitute()
        {
            MockRepositoryInstitute = new Mock<IRepositoryInstitute>();

            ManagerInstitute = new ManagerInstitute(
                Mapper,
                LoggerMock.Object,
                MockHelperUser.Object,
                MockRepositoryInstitute.Object);
        }

        [Fact]
        public void GetInstituteByStudentId_NotStudent()
        {
            MockHelperUser
                .Setup(x => x.CheckRoleStudent(999))
                .Throws<EducationSystemException>();

            Assert.Throws<EducationSystemException>(
                () => ManagerInstitute.GetInstituteByStudentId(999, new OptionsInstitute()));
        }

        [Fact]
        public void GetInstituteByStudentId_Found()
        {
            MockHelperUser.Reset();

            MockRepositoryInstitute
                .Setup(x => x.GetInstituteByStudentId(999))
                .Returns(new DatabaseInstitute { Name = "ИИТиУвТС" });

            var institute = ManagerInstitute.GetInstituteByStudentId(999, new OptionsInstitute());

            Assert.Equal("ИИТиУвТС", institute.Name);
        }

        [Fact]
        public void GetInstituteByStudentId_NotFound()
        {
            MockHelperUser.Reset();

            MockRepositoryInstitute
                .Setup(x => x.GetInstituteByStudentId(999))
                .Returns((DatabaseInstitute) null);

            Assert.Throws<EducationSystemNotFoundException>(
                () => ManagerInstitute.GetInstituteByStudentId(999, new OptionsInstitute()));
        }
    }
}