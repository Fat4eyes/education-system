using System.Collections.Generic;
using System.Linq;
using EducationSystem.Database.Models.Source;
using EducationSystem.Exceptions.Source;
using EducationSystem.Helpers.Interfaces.Source;
using EducationSystem.Managers.Implementations.Source.Rest;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Moq;
using Xunit;

namespace EducationSystem.Tests.Source.Managers.Rest
{
    public class TestsManagerQuestion : TestsManager<ManagerQuestion>
    {
        protected IManagerQuestion ManagerQuestion { get; }

        protected Mock<IHelperQuestion> MockHelperQuestion { get; }

        protected Mock<IRepositoryAnswer> MockRepositoryAnswer { get; }
        protected Mock<IRepositoryProgram> MockRepositoryProgram { get;  }
        protected Mock<IRepositoryQuestion> MockRepositoryQuestion { get;  }
        protected Mock<IRepositoryProgramData> MockRepositoryProgramData { get;  }

        public TestsManagerQuestion()
        {
            MockHelperQuestion = new Mock<IHelperQuestion>();

            MockRepositoryAnswer = new Mock<IRepositoryAnswer>();
            MockRepositoryProgram = new Mock<IRepositoryProgram>();
            MockRepositoryQuestion = new Mock<IRepositoryQuestion>();
            MockRepositoryProgramData = new Mock<IRepositoryProgramData>();

            ManagerQuestion = new ManagerQuestion(
                Mapper,
                LoggerMock.Object,
                MockHelperUser.Object,
                MockHelperQuestion.Object,
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

            var questions = ManagerQuestion.GetQuestionsForStudentByTestId(
                999, 999, new FilterQuestion { Count = 5 });

            Assert.Equal(5, questions.Count);

            Assert.True(questions.All(x => x.Answers.All(y => y.IsRight == null)));
        }

        [Fact]
        public void GetQuestionsForStudentByTestId_NotFound()
        {
            MockHelperUser.Reset();

            MockRepositoryQuestion
                .Setup(x => x.GetQuestionsForStudentByTestId(999, 999, It.IsAny<FilterQuestion>()))
                .Returns(new List<DatabaseQuestion>());

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