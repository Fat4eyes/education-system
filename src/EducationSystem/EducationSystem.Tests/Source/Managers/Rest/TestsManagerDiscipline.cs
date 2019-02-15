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
    public class TestsManagerDiscipline : TestsManager<ManagerDiscipline>
    {
        protected IManagerDiscipline ManagerDiscipline { get; }

        protected Mock<IRepositoryDiscipline> MockRepositoryDiscipline { get; set; }

        protected Mock<IUserHelper> MockUserHelper { get; set; }

        public TestsManagerDiscipline()
        {
            MockRepositoryDiscipline = new Mock<IRepositoryDiscipline>();

            MockUserHelper = new Mock<IUserHelper>();

            ManagerDiscipline = new ManagerDiscipline(
                Mapper,
                LoggerMock.Object,
                MockUserHelper.Object,
                MockRepositoryDiscipline.Object);
        }

        [Fact]
        public void GetDisciplineById_Found()
        {
            MockRepositoryDiscipline
                .Setup(x => x.GetDisciplineById(999, It.IsAny<OptionsDiscipline>()))
                .Returns(new DatabaseDiscipline { Name = "WEB" });

            var discipline = ManagerDiscipline.GetDisciplineById(999, new OptionsDiscipline());

            Assert.Equal("WEB", discipline.Name);
        }

        [Fact]
        public void GetDisciplineById_NotFound()
        {
            MockRepositoryDiscipline
                .Setup(x => x.GetDisciplineById(999, It.IsAny<OptionsDiscipline>()))
                .Returns((DatabaseDiscipline) null);

            Assert.Throws<EducationSystemNotFoundException>(
                () => ManagerDiscipline.GetDisciplineById(999, new OptionsDiscipline()));
        }
    }
}