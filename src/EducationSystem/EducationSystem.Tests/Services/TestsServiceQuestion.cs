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

        [Fact]
        public async Task DeleteQuestion()
        {
            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(Creator.CreateAdmin);

            RepositoryQuestion
                .Setup(x => x.FindFirstAsync(It.IsAny<ISpecification<DatabaseQuestion>>()))
                .ReturnsAsync(Creator.CreateDatabaseQuestion());

            await ServiceQuestion.DeleteQuestionAsync(999);

            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(Creator.CreateLecturer);

            RepositoryQuestion
                .Setup(x => x.FindFirstAsync(It.IsAny<ISpecification<DatabaseQuestion>>()))
                .ReturnsAsync(Creator.CreateDatabaseQuestion());

            await ServiceQuestion.DeleteQuestionAsync(999);
        }

        [Fact]
        public async Task DeleteQuestion_Lecturer_NoAccess()
        {
            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(Creator.CreateLecturer);

            RepositoryQuestion
                .Setup(x => x.FindFirstAsync(It.IsAny<ISpecification<DatabaseQuestion>>()))
                .ReturnsAsync(Creator.CreateDatabaseQuestion(lecturerId: 1));

            await Assert.ThrowsAsync<EducationSystemPublicException>
                (() => ServiceQuestion.DeleteQuestionAsync(999));
        }

        [Fact]
        public async Task DeleteQuestion_Student_NoAccess()
        {
            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(Creator.CreateStudent);

            await Assert.ThrowsAsync<EducationSystemPublicException>
                (() => ServiceQuestion.DeleteQuestionAsync(999));
        }

        [Fact]
        public async Task DeleteQuestion_NotFound()
        {
            RepositoryQuestion
                .Setup(x => x.FindFirstAsync(It.IsAny<ISpecification<DatabaseQuestion>>()))
                .ReturnsAsync((DatabaseQuestion) null);

            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(Creator.CreateAdmin);

            await Assert.ThrowsAsync<EducationSystemNotFoundException>
                (() => ServiceQuestion.DeleteQuestionAsync(999));

            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(Creator.CreateLecturer);

            await Assert.ThrowsAsync<EducationSystemNotFoundException>
                (() => ServiceQuestion.DeleteQuestionAsync(999));
        }

        [Fact]
        public async Task CreateQuestion()
        {
            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(Creator.CreateLecturer);

            RepositoryQuestion
                .Setup(x => x.AddAsync(It.Is<DatabaseQuestion>(y => y.Order == int.MaxValue), true))
                .ReturnsAsync(new DatabaseQuestion());

            await ServiceQuestion.CreateQuestionAsync(new Question());
        }

        [Fact]
        public async Task CreateQuestion_NoAccess()
        {
            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(Creator.CreateStudent);

            await Assert.ThrowsAsync<EducationSystemPublicException>
                (() => ServiceQuestion.CreateQuestionAsync(new Question()));

            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(Creator.CreateStudent);

            await Assert.ThrowsAsync<EducationSystemPublicException>
                (() => ServiceQuestion.CreateQuestionAsync(new Question()));
        }

        [Fact]
        public async Task UpdateQuestion()
        {
            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(Creator.CreateLecturer);

            RepositoryQuestion
                .Setup(x => x.FindFirstAsync(It.IsAny<ISpecification<DatabaseQuestion>>()))
                .ReturnsAsync(Creator.CreateDatabaseQuestion());

            await ServiceQuestion.UpdateQuestionAsync(999, new Question());
        }

        [Fact]
        public async Task UpdateQuestion_NoAccess()
        {
            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(Creator.CreateAdmin);

            await Assert.ThrowsAsync<EducationSystemPublicException>
                (() => ServiceQuestion.UpdateQuestionAsync(999, new Question()));

            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(Creator.CreateStudent);

            await Assert.ThrowsAsync<EducationSystemPublicException>
                (() => ServiceQuestion.UpdateQuestionAsync(999, new Question()));

            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(Creator.CreateLecturer);

            RepositoryQuestion
                .Setup(x => x.FindFirstAsync(It.IsAny<ISpecification<DatabaseQuestion>>()))
                .ReturnsAsync(Creator.CreateDatabaseQuestion(lecturerId: 1));

            await Assert.ThrowsAsync<EducationSystemPublicException>
                (() => ServiceQuestion.UpdateQuestionAsync(999, new Question()));
        }

        [Fact]
        public async Task UpdateQuestion_NotFound()
        {
            Context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(Creator.CreateLecturer);

            RepositoryQuestion
                .Setup(x => x.FindFirstAsync(It.IsAny<ISpecification<DatabaseQuestion>>()))
                .ReturnsAsync((DatabaseQuestion) null);

            await Assert.ThrowsAsync<EducationSystemNotFoundException>
                (() => ServiceQuestion.UpdateQuestionAsync(999, new Question()));
        }
    }
}