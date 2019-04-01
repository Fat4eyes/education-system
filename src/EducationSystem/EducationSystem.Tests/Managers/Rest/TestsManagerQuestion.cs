using System.Collections.Generic;
using System.Linq;
using EducationSystem.Database.Models;
using EducationSystem.Enums;
using EducationSystem.Exceptions;
using EducationSystem.Implementations.Managers.Rest;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Managers.Rest;
using EducationSystem.Models.Filters;
using EducationSystem.Repositories.Interfaces;
using Moq;
using Xunit;

namespace EducationSystem.Tests.Managers.Rest
{
    public class TestsManagerQuestion : TestsManager<ManagerQuestion>
    {
        protected IManagerQuestion ManagerQuestion { get; }

        protected Mock<IHelperQuestion> MockHelperQuestion { get; }
        protected Mock<IHelperQuestionTemplate> MockHelperQuestionTemplate { get; }

        protected Mock<IRepositoryAnswer> MockRepositoryAnswer { get; }
        protected Mock<IRepositoryProgram> MockRepositoryProgram { get;  }
        protected Mock<IRepositoryQuestion> MockRepositoryQuestion { get;  }
        protected Mock<IRepositoryProgramData> MockRepositoryProgramData { get;  }

        public TestsManagerQuestion()
        {
            MockHelperQuestion = new Mock<IHelperQuestion>();
            MockHelperQuestionTemplate = new Mock<IHelperQuestionTemplate>();

            MockRepositoryAnswer = new Mock<IRepositoryAnswer>();
            MockRepositoryProgram = new Mock<IRepositoryProgram>();
            MockRepositoryQuestion = new Mock<IRepositoryQuestion>();
            MockRepositoryProgramData = new Mock<IRepositoryProgramData>();

            ManagerQuestion = new ManagerQuestion(
                Mapper,
                LoggerMock.Object,
                MockHelperUser.Object,
                MockHelperQuestion.Object,
                MockHelperQuestionTemplate.Object,
                MockRepositoryAnswer.Object,
                MockRepositoryProgram.Object,
                MockRepositoryQuestion.Object,
                MockRepositoryProgramData.Object);
        }

        [Fact]
        public void GetQuestionsForStudentByTestId_NotStudent()
        {
            MockHelperUser
                .Setup(x => x.CheckRoleStudent(999))
                .Throws<EducationSystemException>();

            Assert.Throws<EducationSystemException>(
                () => ManagerQuestion.GetQuestionsForStudentByTestId(999, 999, new FilterQuestion()));
        }

        [Fact]
        public void GetQuestionsForStudentByTestId_Found()
        {
            MockHelperUser.Reset();

            MockRepositoryQuestion
                .Setup(x => x.GetQuestionsForStudentByTestId(999, 999, It.IsAny<FilterQuestion>()))
                .Returns(GetQuestions());

            var count = GetQuestions().Count;

            var templates = new Dictionary<QuestionType, int>
            {
                { QuestionType.ClosedManyAnswers, count }
            };

            MockHelperQuestionTemplate
                .Setup(x => x.GetTemplates(TestSize.XS, It.IsAny<List<DatabaseQuestion>>()))
                .Returns(templates);

            var questions = ManagerQuestion.GetQuestionsForStudentByTestId(
                999, 999, new FilterQuestion { TestSize = TestSize.XS });

            Assert.Equal(count, questions.Count);

            Assert.True(questions.All(x => x.Answers.All(y => y.IsRight == null)));
        }

        [Fact]
        public void GetQuestionsForStudentByTestId_NotFound()
        {
            MockHelperUser.Reset();

            MockRepositoryQuestion
                .Setup(x => x.GetQuestionsForStudentByTestId(999, 999, It.IsAny<FilterQuestion>()))
                .Returns(new List<DatabaseQuestion>());

            MockHelperQuestionTemplate
                .Setup(x => x.GetTemplates(TestSize.S, It.IsAny<List<DatabaseQuestion>>()))
                .Returns(new Dictionary<QuestionType, int>());

            var questions = ManagerQuestion.GetQuestionsForStudentByTestId(999, 999, new FilterQuestion());

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