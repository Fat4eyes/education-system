using System.Collections.Generic;
using System.Threading.Tasks;
using EducationSystem.Database.Models;
using EducationSystem.Exceptions;
using EducationSystem.Implementations.Services;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Interfaces.Services;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models.Rest;
using EducationSystem.Specifications;
using Moq;
using Xunit;

namespace EducationSystem.Tests.Services
{
    public class TestsServiceMaterial : TestsService<ServiceMaterial>
    {
        protected readonly IServiceMaterial ServiceMaterial;

        protected readonly Mock<IValidator<Material>> ValidatorMaterial
            = new Mock<IValidator<Material>>();

        protected readonly Mock<IRepository<DatabaseMaterial>> RepositoryMaterial
            = new Mock<IRepository<DatabaseMaterial>>();

        protected readonly Mock<IRepository<DatabaseMaterialFile>> RepositoryMaterialFile
            = new Mock<IRepository<DatabaseMaterialFile>>();

        public TestsServiceMaterial()
        {
            ServiceMaterial = new ServiceMaterial(
                Mapper,
                Context.Object,
                Logger.Object,
                ValidatorMaterial.Object,
                RepositoryMaterial.Object,
                RepositoryMaterialFile.Object);
        }

        [Fact]
        public async Task DeleteMaterial_Student_NoAccess()
        {
            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(Creator.CreateStudent);

            await Assert.ThrowsAsync<EducationSystemPublicException>
                (() => ServiceMaterial.DeleteMaterialAsync(999));
        }

        [Fact]
        public async Task DeleteMaterial_Lecturer_NoAccess()
        {
            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(Creator.CreateLecturer);

            RepositoryMaterial
                .Setup(x => x.FindFirstAsync(It.IsAny<ISpecification<DatabaseMaterial>>()))
                .ReturnsAsync(Creator.CreateDatabaseMaterial(1));

            await Assert.ThrowsAsync<EducationSystemPublicException>
                (() => ServiceMaterial.DeleteMaterialAsync(999));
        }

        [Fact]
        public async Task DeleteMaterial_Lecturer()
        {
            var material = Creator.CreateDatabaseMaterial(2);
            var materials = new List<DatabaseMaterial> { material };

            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(Creator.CreateLecturer);

            RepositoryMaterial
                .Setup(x => x.FindFirstAsync(It.IsAny<ISpecification<DatabaseMaterial>>()))
                .ReturnsAsync(material);

            RepositoryMaterial
                .Setup(x => x.RemoveAsync(material, true))
                .Callback(() => materials.Remove(material))
                .Returns(Task.CompletedTask);

            await ServiceMaterial.DeleteMaterialAsync(999);

            Assert.Empty(materials);
        }

        [Fact]
        public async Task DeleteMaterial_Admin()
        {
            var material = Creator.CreateDatabaseMaterial(2);
            var materials = new List<DatabaseMaterial> { material };

            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(Creator.CreateAdmin);

            RepositoryMaterial
                .Setup(x => x.FindFirstAsync(It.IsAny<ISpecification<DatabaseMaterial>>()))
                .ReturnsAsync(material);

            RepositoryMaterial
                .Setup(x => x.RemoveAsync(material, true))
                .Callback(() => materials.Remove(material))
                .Returns(Task.CompletedTask);

            await ServiceMaterial.DeleteMaterialAsync(999);

            Assert.Empty(materials);
        }

        [Fact]
        public async Task DeleteMaterial_NotFound()
        {
            RepositoryMaterial
                .Setup(x => x.FindFirstAsync(It.IsAny<ISpecification<DatabaseMaterial>>()))
                .ReturnsAsync((DatabaseMaterial) null);

            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(Creator.CreateAdmin);

            await Assert.ThrowsAsync<EducationSystemNotFoundException>
                (() => ServiceMaterial.DeleteMaterialAsync(999));

            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(Creator.CreateLecturer);

            await Assert.ThrowsAsync<EducationSystemNotFoundException>
                (() => ServiceMaterial.DeleteMaterialAsync(999));
        }
    }
}