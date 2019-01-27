using Moq;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using EducationSystem.Database.Models.Source;
using EducationSystem.Exceptions.Source;
using EducationSystem.Managers.Implementations.Source.Rest;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Repositories.Interfaces.Source.Rest;

namespace EducationSystem.Tests.Source
{
    public class TestsManagerInstitute : TestsManager
    {
        protected IManagerInstitute ManagerInstitute { get; }

        protected Mock<IRepositoryInstitute> MockRepositoryInstitute { get; set; }

        public TestsManagerInstitute()
        {
            MockRepositoryInstitute = new Mock<IRepositoryInstitute>();

            ManagerInstitute = new ManagerInstitute(Mapper, MockRepositoryInstitute.Object);
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

            var institutes = ManagerInstitute.GetAll();

            Assert.Equal("Институт Б", institutes.Last().Name);
            Assert.Equal("Институт А", institutes.First().Name);
        }

        [Fact]
        public void GetById_InstituteExists()
        {
            MockRepositoryInstitute
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(new DatabaseInstitute { Id = 3, Name = "Институт А" });

            var institute = ManagerInstitute.GetById(5);

            Assert.Equal("Институт А", institute.Name);
        }

        [Fact]
        public void GetById_InstituteNotExists()
        {
            MockRepositoryInstitute
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns((DatabaseInstitute) null);

            Assert.Throws<EducationSystemNotFoundException>(() => ManagerInstitute.GetById(9));
        }
    }
}