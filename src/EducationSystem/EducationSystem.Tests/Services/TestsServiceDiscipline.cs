﻿using System.Threading.Tasks;
using EducationSystem.Database.Models;
using EducationSystem.Exceptions;
using EducationSystem.Implementations.Services;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Interfaces.Services;
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
        public async Task GetDiscipline()
        {
            RepositoryDiscipline
                .Setup(x => x.FindFirstAsync(It.IsAny<ISpecification<DatabaseDiscipline>>()))
                .ReturnsAsync(Creator.CreateDatabaseDiscipline());

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