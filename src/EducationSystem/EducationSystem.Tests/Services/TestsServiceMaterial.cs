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

        protected readonly Mock<IValidator<Material>> ValidatorMaterial = new Mock<IValidator<Material>>();
        protected readonly Mock<IRepository<DatabaseMaterial>> RepositoryMaterial = new Mock<IRepository<DatabaseMaterial>>();
        protected readonly Mock<IRepository<DatabaseMaterialFile>> RepositoryMaterialFile = new Mock<IRepository<DatabaseMaterialFile>>();
        protected readonly Mock<IRepository<DatabaseMaterialAnchor>> RepositoryMaterialAnchor = new Mock<IRepository<DatabaseMaterialAnchor>>();

        public TestsServiceMaterial()
        {
            ServiceMaterial = new ServiceMaterial(
                Mapper,
                Context.Object,
                Logger.Object,
                ValidatorMaterial.Object,
                RepositoryMaterial.Object,
                RepositoryMaterialFile.Object,
                RepositoryMaterialAnchor.Object);
        }

        [Fact]
        public async Task GetMaterial()
        {
            RepositoryMaterial
                .Setup(x => x.FindFirstAsync(It.IsAny<ISpecification<DatabaseMaterial>>()))
                .ReturnsAsync(Creator.CreateDatabaseMaterial(ownerId: 2));

            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(Creator.CreateAdmin);

            var material = await ServiceMaterial.GetMaterialAsync(999);

            Assert.Equal(2, material.OwnerId);

            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(Creator.CreateStudent);

            material = await ServiceMaterial.GetMaterialAsync(999);

            Assert.Equal(2, material.OwnerId);

            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(Creator.CreateLecturer);

            material = await ServiceMaterial.GetMaterialAsync(999);

            Assert.Equal(2, material.OwnerId);
        }

        [Fact]
        public async Task GetMaterial_Lecturer_NoAccess()
        {
            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(Creator.CreateLecturer);

            RepositoryMaterial
                .Setup(x => x.FindFirstAsync(It.IsAny<ISpecification<DatabaseMaterial>>()))
                .ReturnsAsync(Creator.CreateDatabaseMaterial(ownerId: 1));

            await Assert.ThrowsAsync<EducationSystemPublicException>
                (() => ServiceMaterial.GetMaterialAsync(999));
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
                .ReturnsAsync(Creator.CreateDatabaseMaterial(ownerId: 1));

            await Assert.ThrowsAsync<EducationSystemPublicException>
                (() => ServiceMaterial.DeleteMaterialAsync(999));
        }

        [Fact]
        public async Task DeleteMaterial_Lecturer()
        {
            var material = Creator.CreateDatabaseMaterial(ownerId: 2);
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
            var material = Creator.CreateDatabaseMaterial(ownerId: 2);
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

        [Fact]
        public async Task UpdateMaterial_NoAccess()
        {
            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(Creator.CreateAdmin);

            await Assert.ThrowsAsync<EducationSystemPublicException>
                (() => ServiceMaterial.UpdateMaterialAsync(999, new Material()));

            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(Creator.CreateStudent);

            await Assert.ThrowsAsync<EducationSystemPublicException>
                (() => ServiceMaterial.UpdateMaterialAsync(999, new Material()));

            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(Creator.CreateLecturer);

            RepositoryMaterial
                .Setup(x => x.FindFirstAsync(It.IsAny<ISpecification<DatabaseMaterial>>()))
                .ReturnsAsync(Creator.CreateDatabaseMaterial(ownerId: 1));

            await Assert.ThrowsAsync<EducationSystemPublicException>
                (() => ServiceMaterial.UpdateMaterialAsync(999, new Material()));
        }

        [Fact]
        public async Task UpdateMaterial_NotFound()
        {
            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(Creator.CreateLecturer);

            RepositoryMaterial
                .Setup(x => x.FindFirstAsync(It.IsAny<ISpecification<DatabaseMaterial>>()))
                .ReturnsAsync((DatabaseMaterial) null);

            await Assert.ThrowsAsync<EducationSystemNotFoundException>
                (() => ServiceMaterial.UpdateMaterialAsync(999, new Material()));
        }

        [Fact]
        public async Task CreateMaterial_NoAccess()
        {
            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(Creator.CreateAdmin);

            await Assert.ThrowsAsync<EducationSystemPublicException>
                (() => ServiceMaterial.CreateMaterialAsync(new Material()));

            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(Creator.CreateStudent);

            await Assert.ThrowsAsync<EducationSystemPublicException>
                (() => ServiceMaterial.CreateMaterialAsync(new Material()));
        }

        [Fact]
        public async Task CreateMaterial()
        {
            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(Creator.CreateLecturer);

            RepositoryMaterial
                .Setup(x => x.AddAsync(It.Is<DatabaseMaterial>(y => y.OwnerId == 2), true))
                .ReturnsAsync(new DatabaseMaterial());

            await ServiceMaterial.CreateMaterialAsync(new Material());
        }
    }
}