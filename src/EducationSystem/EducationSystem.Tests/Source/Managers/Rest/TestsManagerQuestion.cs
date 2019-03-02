using System.Collections.Generic;
using System.Linq;
using EducationSystem.Database.Models.Source;
using EducationSystem.Exceptions.Source;
using EducationSystem.Managers.Implementations.Source.Rest;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Moq;
using Xunit;

namespace EducationSystem.Tests.Source.Managers.Rest
{
    public class TestsManagerQuestion : TestsManager<ManagerQuestion>
    {
        protected IManagerQuestion ManagerQuestion { get; }

        protected Mock<IRepositoryQuestion> MockRepositoryQuestion { get; set; }

        public TestsManagerQuestion()
        {
            MockRepositoryQuestion = new Mock<IRepositoryQuestion>();

            ManagerQuestion = new ManagerQuestion(
                Mapper,
                LoggerMock.Object,
                MockUserHelper.Object,
                MockRepositoryQuestion.Object);
        }

        [Fact]
        public void GetQuestionsForStudentByTestId_NotStudent()
        {
            MockUserHelper
                .Setup(x => x.CheckRoleStudent(999))
                .Throws<EducationSystemException>();

            Assert.Throws<EducationSystemException>(
                () => ManagerQuestion.GetQuestionsForStudentByTestId(999, 999, 999));
        }

        [Fact]
        public void GetQuestionsForStudentByTestId_Found()
        {
            MockUserHelper.Reset();

            MockRepositoryQuestion
                .Setup(x => x.GetQuestionsForStudentByTestId(999, 999))
                .Returns(GetQuestions());

            var questions = ManagerQuestion.GetQuestionsForStudentByTestId(999, 999, 5);

            Assert.Equal(5, questions.Count);

            Assert.True(questions.All(x => x.Answers.All(y => y.IsRight == null)));
        }

        [Fact]
        public void GetQuestionsForStudentByTestId_NotFound()
        {
            MockUserHelper.Reset();

            MockRepositoryQuestion
                .Setup(x => x.GetQuestionsForStudentByTestId(999, 999))
                .Returns(new List<DatabaseQuestion>());

            var questions = ManagerQuestion.GetQuestionsForStudentByTestId(999, 999, 5);

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