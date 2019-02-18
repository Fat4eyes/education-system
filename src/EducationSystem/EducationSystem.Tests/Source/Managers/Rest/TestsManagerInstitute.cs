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
    public class TestsManagerInstitute : TestsManager<ManagerInstitute>
    {
        protected IManagerInstitute ManagerInstitute { get; }

        protected Mock<IRepositoryInstitute> MockRepositoryInstitute { get; set; }

        protected Mock<IUserHelper> MockUserHelper { get; set; }

        public TestsManagerInstitute()
        {
            MockRepositoryInstitute = new Mock<IRepositoryInstitute>();

            MockUserHelper = new Mock<IUserHelper>();

            ManagerInstitute = new ManagerInstitute(
                Mapper,
                LoggerMock.Object,
                MockUserHelper.Object,
                MockRepositoryInstitute.Object);
        }

        [Fact]
        public void GetInstituteByStudentId_NotStudent()
        {
            MockUserHelper
                .Setup(x => x.IsStudent(999))
                .Returns(false);

            Assert.Throws<EducationSystemException>(
                () => ManagerInstitute.GetInstituteByStudentId(999, new OptionsInstitute()));
        }

        [Fact]
        public void GetInstituteByStudentId_Found()
        {
            MockUserHelper
                .Setup(x => x.IsStudent(999))
                .Returns(true);

            MockRepositoryInstitute
                .Setup(x => x.GetInstituteByStudentId(999))
                .Returns(new DatabaseInstitute { Name = "ИИТиУвТС" });

            var institute = ManagerInstitute.GetInstituteByStudentId(999, new OptionsInstitute());

            Assert.Equal("ИИТиУвТС", institute.Name);
        }

        [Fact]
        public void GetInstituteByStudentId_NotFound()
        {
            MockUserHelper
                .Setup(x => x.IsStudent(999))
                .Returns(true);

            MockRepositoryInstitute
                .Setup(x => x.GetInstituteByStudentId(999))
                .Returns((DatabaseInstitute) null);

            Assert.Throws<EducationSystemNotFoundException>(
                () => ManagerInstitute.GetInstituteByStudentId(999, new OptionsInstitute()));
        }
    }
}