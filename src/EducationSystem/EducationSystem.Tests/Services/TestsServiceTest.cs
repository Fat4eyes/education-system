using System.Threading.Tasks;
using EducationSystem.Database.Models;
using EducationSystem.Exceptions;
using EducationSystem.Implementations.Services;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Interfaces.Services;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models.Rest;
using EducationSystem.Specifications;
using EducationSystem.Tests.Helpers;
using Moq;
using Xunit;

namespace EducationSystem.Tests.Services
{
    public class TestsServiceTest : TestsService<ServiceTest>
    {
        protected readonly IServiceTest ServiceTest;
        protected readonly Mock<IValidator<Test>> ValidatorTest = new Mock<IValidator<Test>>();
        protected readonly Mock<IRepository<DatabaseTest>> RepositoryTest = new Mock<IRepository<DatabaseTest>>();
        protected readonly Mock<IRepository<DatabaseQuestion>> RepositoryQuestion = new Mock<IRepository<DatabaseQuestion>>();
        protected readonly Mock<IRepository<DatabaseTestTheme>> RepositoryTestTheme = new Mock<IRepository<DatabaseTestTheme>>();
        protected readonly Mock<IRepository<DatabaseQuestionStudent>> RepositoryQuestionStudent = new Mock<IRepository<DatabaseQuestionStudent>>();

        public TestsServiceTest()
        {
            ServiceTest = new ServiceTest(
                Mapper,
                Context.Object,
                Logger.Object,
                ValidatorTest.Object,
                RepositoryTest.Object,
                RepositoryQuestion.Object,
                RepositoryTestTheme.Object,
                RepositoryQuestionStudent.Object);
        }

        [Fact]
        public async Task GetTest_NotFound()
        {
            RepositoryTest
                .Setup(x => x.FindFirstAsync(It.IsAny<ISpecification<DatabaseTest>>()))
                .ReturnsAsync((DatabaseTest) null);

            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(ModelsCreationHelper.CreateAdmin);

            await Assert.ThrowsAsync<EducationSystemNotFoundException>
                (() => ServiceTest.GetTestAsync(999));

            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(ModelsCreationHelper.CreateLecturer);

            await Assert.ThrowsAsync<EducationSystemNotFoundException>
                (() => ServiceTest.GetTestAsync(999));

            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(ModelsCreationHelper.CreateStudent);

            await Assert.ThrowsAsync<EducationSystemNotFoundException>
                (() => ServiceTest.GetTestAsync(999));
        }

        [Fact]
        public async Task GetTest_Student_NoAccess()
        {
            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(ModelsCreationHelper.CreateStudent);

            RepositoryTest
                .Setup(x => x.FindFirstAsync(It.IsAny<ISpecification<DatabaseTest>>()))
                .ReturnsAsync(ModelsCreationHelper.CreateDatabaseTest(isActive: true, studentId: 1));

            await Assert.ThrowsAsync<EducationSystemPublicException>
                (() => ServiceTest.GetTestAsync(999));

            RepositoryTest
                .Setup(x => x.FindFirstAsync(It.IsAny<ISpecification<DatabaseTest>>()))
                .ReturnsAsync(ModelsCreationHelper.CreateDatabaseTest());

            await Assert.ThrowsAsync<EducationSystemPublicException>
                (() => ServiceTest.GetTestAsync(999));
        }

        [Fact]
        public async Task GetTest_Lecturer_NoAccess()
        {
            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(ModelsCreationHelper.CreateLecturer);

            RepositoryTest
                .Setup(x => x.FindFirstAsync(It.IsAny<ISpecification<DatabaseTest>>()))
                .ReturnsAsync(ModelsCreationHelper.CreateDatabaseTest(lecturerId: 1));

            await Assert.ThrowsAsync<EducationSystemPublicException>
                (() => ServiceTest.GetTestAsync(999));
        }

        [Fact]
        public async Task DeleteTest()
        {
            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(ModelsCreationHelper.CreateAdmin);

            RepositoryTest
                .Setup(x => x.FindFirstAsync(It.IsAny<ISpecification<DatabaseTest>>()))
                .ReturnsAsync(ModelsCreationHelper.CreateDatabaseTest());

            await ServiceTest.DeleteTestAsync(999);

            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(ModelsCreationHelper.CreateLecturer);

            RepositoryTest
                .Setup(x => x.FindFirstAsync(It.IsAny<ISpecification<DatabaseTest>>()))
                .ReturnsAsync(ModelsCreationHelper.CreateDatabaseTest());

            await ServiceTest.DeleteTestAsync(999);
        }

        [Fact]
        public async Task DeleteTest_Student_NoAccess()
        {
            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(ModelsCreationHelper.CreateStudent);

            await Assert.ThrowsAsync<EducationSystemPublicException>
                (() => ServiceTest.DeleteTestAsync(999));
        }

        [Fact]
        public async Task DeleteTest_Lecturer_NoAccess()
        {
            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(ModelsCreationHelper.CreateStudent);

            RepositoryTest
                .Setup(x => x.FindFirstAsync(It.IsAny<ISpecification<DatabaseTest>>()))
                .ReturnsAsync(ModelsCreationHelper.CreateDatabaseTest(lecturerId: 1));

            await Assert.ThrowsAsync<EducationSystemPublicException>
                (() => ServiceTest.DeleteTestAsync(999));
        }

        [Fact]
        public async Task DeleteTest_NotFound()
        {
            RepositoryTest
                .Setup(x => x.FindFirstAsync(It.IsAny<ISpecification<DatabaseTest>>()))
                .ReturnsAsync((DatabaseTest) null);

            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(ModelsCreationHelper.CreateAdmin);

            await Assert.ThrowsAsync<EducationSystemNotFoundException>
                (() => ServiceTest.DeleteTestAsync(999));

            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(ModelsCreationHelper.CreateLecturer);

            await Assert.ThrowsAsync<EducationSystemNotFoundException>
                (() => ServiceTest.DeleteTestAsync(999));
        }

        [Fact]
        public async Task CreateTest()
        {
            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(ModelsCreationHelper.CreateLecturer);

            await ServiceTest.CreateTestAsync(new Test());
        }

        [Fact]
        public async Task CreateTest_NoAccess()
        {
            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(ModelsCreationHelper.CreateAdmin);

            await Assert.ThrowsAsync<EducationSystemPublicException>
                (() => ServiceTest.CreateTestAsync(new Test()));

            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(ModelsCreationHelper.CreateStudent);

            await Assert.ThrowsAsync<EducationSystemPublicException>
                (() => ServiceTest.CreateTestAsync(new Test()));
        }

        [Fact]
        public async Task UpdateTest()
        {
            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(ModelsCreationHelper.CreateLecturer);

            RepositoryTest
                .Setup(x => x.FindFirstAsync(It.IsAny<ISpecification<DatabaseTest>>()))
                .ReturnsAsync(ModelsCreationHelper.CreateDatabaseTest());

            await ServiceTest.UpdateTestAsync(999, new Test());
        }

        [Fact]
        public async Task UpdateTest_NoAccess()
        {
            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(ModelsCreationHelper.CreateAdmin);

            await Assert.ThrowsAsync<EducationSystemPublicException>
                (() => ServiceTest.UpdateTestAsync(999, new Test()));

            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(ModelsCreationHelper.CreateStudent);

            await Assert.ThrowsAsync<EducationSystemPublicException>
                (() => ServiceTest.UpdateTestAsync(999, new Test()));

            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(ModelsCreationHelper.CreateLecturer);

            RepositoryTest
                .Setup(x => x.FindFirstAsync(It.IsAny<ISpecification<DatabaseTest>>()))
                .ReturnsAsync(ModelsCreationHelper.CreateDatabaseTest(lecturerId: 1));

            await Assert.ThrowsAsync<EducationSystemPublicException>
                (() => ServiceTest.UpdateTestAsync(999, new Test()));
        }

        [Fact]
        public async Task UpdateTest_NotFound()
        {
            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(ModelsCreationHelper.CreateLecturer);

            RepositoryTest
                .Setup(x => x.FindFirstAsync(It.IsAny<ISpecification<DatabaseTest>>()))
                .ReturnsAsync((DatabaseTest) null);

            await Assert.ThrowsAsync<EducationSystemNotFoundException>
                (() => ServiceTest.UpdateTestAsync(999, new Test()));
        }
    }
}