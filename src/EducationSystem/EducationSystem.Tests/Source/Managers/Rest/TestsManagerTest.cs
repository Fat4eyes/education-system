using System.Collections.Generic;
using EducationSystem.Database.Models.Source;
using EducationSystem.Exceptions.Source;
using EducationSystem.Implementations.Managers.Rest;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Managers.Rest;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Models.Source.Options;
using EducationSystem.Repositories.Interfaces;
using Moq;
using Xunit;

namespace EducationSystem.Tests.Source.Managers.Rest
{
    public class TestsManagerTest : TestsManager<ManagerTest>
    {
        protected IManagerTest ManagerTest { get; }

        protected Mock<IRepositoryTest> MockRepositoryTest { get; }
        protected Mock<IRepositoryTestTheme> RepositoryTestTheme { get; }

        protected Mock<IHelperTest> MockTestHelper { get; }

        public TestsManagerTest()
        {
            MockRepositoryTest = new Mock<IRepositoryTest>();
            RepositoryTestTheme = new Mock<IRepositoryTestTheme>();

            MockTestHelper = new Mock<IHelperTest>();

            ManagerTest = new ManagerTest(
                Mapper,
                LoggerMock.Object,
                MockHelperUser.Object,
                MockTestHelper.Object,
                MockRepositoryTest.Object,
                RepositoryTestTheme.Object);
        }

        [Fact]
        public void GetTestById_Found()
        {
            MockRepositoryTest
                .Setup(x => x.GetById(999))
                .Returns(new DatabaseTest { Subject = "Subject" });

            var test = ManagerTest.GetTestById(999, new OptionsTest());

            Assert.Equal("Subject", test.Subject);
        }

        [Fact]
        public void GetTetsById_NotFound()
        {
            MockRepositoryTest
                .Setup(x => x.GetById(999))
                .Returns((DatabaseTest) null);

            Assert.Throws<EducationSystemNotFoundException>(
                () => ManagerTest.GetTestById(999, new OptionsTest()));
        }

        [Fact]
        public void GetDisciplinesByStudentId_NotStudent()
        {
            MockHelperUser
                .Setup(x => x.CheckRoleStudent(999))
                .Throws<EducationSystemException>();

            Assert.Throws<EducationSystemException>(
                () => ManagerTest.GetTestsForStudent(999, new OptionsTest(), new FilterTest()));
        }

        [Fact]
        public void GetTestsByStudentId_FoundWithThemes()
        {
            MockHelperUser.Reset();

            var tests = GetTests();

            MockRepositoryTest
                .Setup(x => x.GetTestsForStudent(999, It.IsAny<FilterTest>()))
                .Returns((tests.Count, tests));

            var data = ManagerTest.GetTestsForStudent
                (999, new OptionsTest { WithThemes = true }, new FilterTest());

            Assert.Equal(3, data.Count);

            Assert.Equal(2, data.Items[0].Themes.Count);

            Assert.Single(data.Items[1].Themes);
            Assert.Single(data.Items[2].Themes);

            Assert.Equal("Theme", data.Items[1].Themes[0].Name);
            Assert.Equal("Theme", data.Items[2].Themes[0].Name);
        }

        private static List<DatabaseTest> GetTests()
        {
            return new List<DatabaseTest>
            {
                Creator.CreateTest(
                    Creator.CreateTheme(
                        new DatabaseQuestion(),
                        new DatabaseQuestion(),
                        new DatabaseQuestion()),
                    Creator.CreateTheme(
                        new DatabaseQuestion(),
                        new DatabaseQuestion(),
                        new DatabaseQuestion())),
                Creator.CreateTest(
                    Creator.CreateTheme(),
                    Creator.CreateTheme(
                        new DatabaseQuestion(),
                        new DatabaseQuestion(),
                        new DatabaseQuestion())),
                Creator.CreateTest(
                    Creator.CreateTheme(),
                    Creator.CreateTheme(),
                    Creator.CreateTheme(
                        new DatabaseQuestion(),
                        new DatabaseQuestion(),
                        new DatabaseQuestion()))
            };
        }
    }
}