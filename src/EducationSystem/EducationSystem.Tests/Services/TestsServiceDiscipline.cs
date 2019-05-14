using System.Threading.Tasks;
using EducationSystem.Database.Models;
using EducationSystem.Exceptions;
using EducationSystem.Implementations.Services;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Interfaces.Services;
using EducationSystem.Models.Filters;
using EducationSystem.Specifications;
using Moq;
using Xunit;

namespace EducationSystem.Tests.Services
{
    public class TestsServiceDiscipline : TestsService<ServiceDiscipline>
    {
        protected readonly IServiceDiscipline ServiceDiscipline;

        protected readonly Mock<IRepository<DatabaseDiscipline>> RepositoryDiscipline
            = new Mock<IRepository<DatabaseDiscipline>>();

        public TestsServiceDiscipline()
        {
            ServiceDiscipline = new ServiceDiscipline(
                Mapper,
                Context.Object,
                Logger.Object,
                RepositoryDiscipline.Object
            );
        }

        [Fact]
        public async Task GetDisciplines()
        {
            var filter = new FilterDiscipline();

            RepositoryDiscipline
                .Setup(x => x.FindPaginatedAsync(It.IsAny<ISpecification<DatabaseDiscipline>>(), filter))
                .ReturnsAsync((4, Creator.CreateDisciplines()));

            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(Creator.CreateAdmin);

            var data = await ServiceDiscipline.GetDisciplinesAsync(filter);

            Assert.Equal(4, data.Count);

            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(Creator.CreateLecturer);

            data = await ServiceDiscipline.GetDisciplinesAsync(filter);

            Assert.Equal(4, data.Count);

            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(Creator.CreateStudent);

            data = await ServiceDiscipline.GetDisciplinesAsync(filter);

            Assert.Equal(4, data.Count);

            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(Creator.CreateEmployee);

            await Assert.ThrowsAsync<EducationSystemPublicException>
                (() => ServiceDiscipline.GetDisciplinesAsync(filter));
        }

        [Fact]
        public async Task GetDiscipline_Found()
        {
            RepositoryDiscipline
                .Setup(x => x.FindFirstAsync(It.IsAny<ISpecification<DatabaseDiscipline>>()))
                .ReturnsAsync(Creator.CreateDiscipline());

            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(Creator.CreateAdmin);

            await ServiceDiscipline.GetDisciplineAsync(999);

            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(Creator.CreateLecturer);

            await ServiceDiscipline.GetDisciplineAsync(999);

            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(Creator.CreateStudent);

            await ServiceDiscipline.GetDisciplineAsync(999);

            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(Creator.CreateEmployee);

            await Assert.ThrowsAsync<EducationSystemPublicException>
                (() => ServiceDiscipline.GetDisciplineAsync(999));
        }

        [Fact]
        public async Task GetDiscipline_NotFound()
        {
            RepositoryDiscipline
                .Setup(x => x.FindFirstAsync(It.IsAny<ISpecification<DatabaseDiscipline>>()))
                .ReturnsAsync((DatabaseDiscipline) null);

            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(Creator.CreateAdmin);

            await Assert.ThrowsAsync<EducationSystemNotFoundException>
                (() => ServiceDiscipline.GetDisciplineAsync(999));

            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(Creator.CreateLecturer);

            await Assert.ThrowsAsync<EducationSystemNotFoundException>
                (() => ServiceDiscipline.GetDisciplineAsync(999));

            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(Creator.CreateStudent);

            await Assert.ThrowsAsync<EducationSystemNotFoundException>
                (() => ServiceDiscipline.GetDisciplineAsync(999));
        }
    }
}