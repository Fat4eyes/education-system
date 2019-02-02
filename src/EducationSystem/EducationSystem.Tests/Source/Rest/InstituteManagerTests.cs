using System.Collections.Generic;
using System.Linq;
using EducationSystem.Database.Models.Source;
using EducationSystem.Exceptions.Source;
using EducationSystem.Managers.Implementations.Source.Rest;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using EducationSystem.Tests.Source.Rest.Base;
using Moq;
using Xunit;

namespace EducationSystem.Tests.Source.Rest
{
    public class InstituteManagerTests : BaseManagerTests<InstituteManager>
    {
        protected IInstituteManager InstituteManager { get; }

        protected Mock<IInstituteRepository> MockRepositoryInstitute { get; }

        public InstituteManagerTests()
        {
            MockRepositoryInstitute = new Mock<IInstituteRepository>();

            InstituteManager = new InstituteManager(
                Mapper,
                LoggerMock.Object,
                MockRepositoryInstitute.Object);
        }

        [Fact]
        public void GetAll()
        {
            MockRepositoryInstitute
                .Setup(x => x.GetAll())
                .Returns(new List<DatabaseInstitute> {
                    new DatabaseInstitute { Id = 3, Name = "Институт А" },
                    new DatabaseInstitute { Id = 5, Name = "Институт Б" }
                });

            var institutes = InstituteManager.GetAll();

            Assert.Equal("Институт Б", institutes.Last().Name);
            Assert.Equal("Институт А", institutes.First().Name);
        }

        [Fact]
        public void GetById_InstituteExists()
        {
            MockRepositoryInstitute
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(new DatabaseInstitute { Id = 3, Name = "Институт А" });

            var institute = InstituteManager.GetById(5);

            Assert.Equal("Институт А", institute.Name);
        }

        [Fact]
        public void GetById_InstituteNotExists()
        {
            MockRepositoryInstitute
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns((DatabaseInstitute) null);

            Assert.Throws<EducationSystemNotFoundException>(() => InstituteManager.GetById(9));
        }
    }
}