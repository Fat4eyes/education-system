using System.Collections.Generic;
using System.Linq;
using EducationSystem.Database.Models;
using EducationSystem.Enums;
using EducationSystem.Exceptions;
using EducationSystem.Implementations.Managers.Rest;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Managers.Rest;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Source.Rest;
using EducationSystem.Repositories.Interfaces;
using Moq;
using Xunit;

namespace EducationSystem.Tests.Managers.Rest
{
    public class TestsManagerQuestion : TestsManager<ManagerQuestion>
    {
        private readonly IManagerQuestion _managerQuestion;

        private readonly Mock<IValidator<Question>> _mockHelperQuestion
            = new Mock<IValidator<Question>>();

        private readonly Mock<IHelperQuestionTemplate> _mockHelperQuestionTemplate
            = new Mock<IHelperQuestionTemplate>();

        private readonly Mock<IRepositoryAnswer> _mockRepositoryAnswer
            = new Mock<IRepositoryAnswer>();

        private readonly Mock<IRepositoryProgram> _mockRepositoryProgram
            = new Mock<IRepositoryProgram>();

        private readonly Mock<IRepositoryQuestion> _mockRepositoryQuestion
            = new Mock<IRepositoryQuestion>();

        private readonly Mock<IRepositoryProgramData> _mockRepositoryProgramData
            = new Mock<IRepositoryProgramData>();

        public TestsManagerQuestion()
        {
            _managerQuestion = new ManagerQuestion(
                Mapper,
                LoggerMock.Object,
                MockHelperUser.Object,
                _mockHelperQuestion.Object,
                _mockHelperQuestionTemplate.Object,
                _mockRepositoryAnswer.Object,
                _mockRepositoryProgram.Object,
                _mockRepositoryQuestion.Object,
                _mockRepositoryProgramData.Object);
        }

        [Fact]
        public void GetQuestionsForStudentByTestId_NotStudent()
        {
            MockHelperUser
                .Setup(x => x.CheckRoleStudent(999))
                .Throws<EducationSystemException>();

            Assert.Throws<EducationSystemException>(
                () => _managerQuestion.GetQuestionsForStudentByTestId(999, 999, new FilterQuestion()));
        }

        [Fact]
        public void GetQuestionsForStudentByTestId_Found()
        {
            MockHelperUser.Reset();

            _mockRepositoryQuestion
                .Setup(x => x.GetQuestionsForStudentByTestId(999, 999, It.IsAny<FilterQuestion>()))
                .Returns(GetQuestions());

            var count = GetQuestions().Count;

            var templates = new Dictionary<QuestionType, int>
            {
                { QuestionType.ClosedManyAnswers, count }
            };

            _mockHelperQuestionTemplate
                .Setup(x => x.GetTemplates(TestSize.XS, It.IsAny<List<DatabaseQuestion>>()))
                .Returns(templates);

            var questions = _managerQuestion.GetQuestionsForStudentByTestId(
                999, 999, new FilterQuestion { TestSize = TestSize.XS });

            Assert.Equal(count, questions.Count);

            Assert.True(questions.All(x => x.Answers.All(y => y.IsRight == null)));
        }

        [Fact]
        public void GetQuestionsForStudentByTestId_NotFound()
        {
            MockHelperUser.Reset();

            _mockRepositoryQuestion
                .Setup(x => x.GetQuestionsForStudentByTestId(999, 999, It.IsAny<FilterQuestion>()))
                .Returns(new List<DatabaseQuestion>());

            _mockHelperQuestionTemplate
                .Setup(x => x.GetTemplates(TestSize.S, It.IsAny<List<DatabaseQuestion>>()))
                .Returns(new Dictionary<QuestionType, int>());

            var questions = _managerQuestion.GetQuestionsForStudentByTestId(999, 999, new FilterQuestion());

            Assert.Empty(questions);
        }

        private static List<DatabaseQuestion> GetQuestions()
        {
            return new List<DatabaseQuestion>
            {
                Creator.CreateQuestion(
                    Creator.CreateAnswer(),
                    Creator.CreateRightAnswer()),
                Creator.CreateQuestion(
                    Creator.CreateAnswer(),
                    Creator.CreateAnswer(),
                    Creator.CreateRightAnswer()),
                Creator.CreateQuestion(
                    Creator.CreateAnswer(),
                    Creator.CreateRightAnswer()),
                Creator.CreateQuestion(
                    Creator.CreateAnswer(),
                    Creator.CreateRightAnswer()),
                Creator.CreateQuestion(
                    Creator.CreateAnswer(),
                    Creator.CreateAnswer(),
                    Creator.CreateAnswer(),
                    Creator.CreateRightAnswer()),
                Creator.CreateQuestion(
                    Creator.CreateAnswer(),
                    Creator.CreateRightAnswer())
            };
        }
    }
}