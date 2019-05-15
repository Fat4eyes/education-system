using System.Threading.Tasks;
using EducationSystem.Database.Models;
using EducationSystem.Exceptions;
using EducationSystem.Implementations.Services;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Interfaces.Services;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models.Rest;
using EducationSystem.Specifications;
using Moq;
using Xunit;

namespace EducationSystem.Tests.Services
{
    public class TestsServiceQuestion : TestsService<ServiceQuestion>
    {
        protected readonly IServiceQuestion ServiceQuestion;

        protected readonly Mock<IValidator<Question>> ValidatorQuestion = new Mock<IValidator<Question>>();
        protected readonly Mock<IRepository<DatabaseTest>> RepositoryTest = new Mock<IRepository<DatabaseTest>>();
        protected readonly Mock<IRepository<DatabaseTheme>> RepositoryTheme = new Mock<IRepository<DatabaseTheme>>();
        protected readonly Mock<IRepository<DatabaseAnswer>> RepositoryAnswer = new Mock<IRepository<DatabaseAnswer>>();
        protected readonly Mock<IRepository<DatabaseProgram>> RepositoryProgram = new Mock<IRepository<DatabaseProgram>>();
        protected readonly Mock<IRepository<DatabaseQuestion>> RepositoryQuestion = new Mock<IRepository<DatabaseQuestion>>();
        protected readonly Mock<IRepository<DatabaseProgramData>> RepositoryProgramData = new Mock<IRepository<DatabaseProgramData>>();
        protected readonly Mock<IQuestionValidatorFactory> QuestionValidatorFactory = new Mock<IQuestionValidatorFactory>();

        public TestsServiceQuestion()
        {
            ServiceQuestion = new ServiceQuestion(
                Mapper,
                Context.Object,
                Logger.Object,
                ValidatorQuestion.Object,
                RepositoryTest.Object,
                RepositoryTheme.Object,
                RepositoryAnswer.Object,
                RepositoryProgram.Object,
                RepositoryQuestion.Object,
                RepositoryProgramData.Object,
                QuestionValidatorFactory.Object);
        }

        [Fact]
        public async Task GetQuestion_Student_NoAccess()
        {
            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(Creator.CreateStudent);

            await Assert.ThrowsAsync<EducationSystemPublicException>
                (() => ServiceQuestion.GetQuestionAsync(999));
        }

        [Fact]
        public async Task GetQuestion_Lecturer_NoAccess()
        {
            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(Creator.CreateStudent);

            RepositoryQuestion
                .Setup(x => x.FindFirstAsync(It.IsAny<ISpecification<DatabaseQuestion>>()))
                .ReturnsAsync(Creator.CreateDatabaseQuestion(lecturerId: 1));

            await Assert.ThrowsAsync<EducationSystemPublicException>
                (() => ServiceQuestion.GetQuestionAsync(999));
        }
    }
}