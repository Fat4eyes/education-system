using System.Collections.Generic;
using EducationSystem.Database.Models;
using EducationSystem.Exceptions;
using EducationSystem.Implementations.Managers.Rest;
using EducationSystem.Interfaces.Managers.Rest;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.Models.Source.Rest;
using EducationSystem.Repositories.Interfaces;
using Moq;
using Xunit;

namespace EducationSystem.Tests.Managers.Rest
{
    public class TestsManagerTest : TestsManager<ManagerTest>
    {
        private readonly IManagerTest _managerTest;

        private readonly Mock<IRepositoryTest> _mockRepositoryTest
            = new Mock<IRepositoryTest>();

        private readonly Mock<IRepositoryTestTheme> _mockRepositoryTestTheme
            = new Mock<IRepositoryTestTheme>();

        private readonly Mock<IValidator<Test>> _mockCheckerTest
            = new Mock<IValidator<Test>>();

        public TestsManagerTest()
        {
            _managerTest = new ManagerTest(
                Mapper,
                LoggerMock.Object,
                MockHelperUser.Object,
                _mockCheckerTest.Object,
                _mockRepositoryTest.Object,
                _mockRepositoryTestTheme.Object);
        }

        [Fact]
        public void GetTestById_Found()
        {
            _mockRepositoryTest
                .Setup(x => x.GetById(999))
                .Returns(new DatabaseTest { Subject = "Subject" });

            var test = _managerTest.GetTestById(999, new OptionsTest());

            Assert.Equal("Subject", test.Subject);
        }

        [Fact]
        public void GetTetsById_NotFound()
        {
            _mockRepositoryTest
                .Setup(x => x.GetById(999))
                .Returns((DatabaseTest) null);

            Assert.Throws<EducationSystemNotFoundException>(
                () => _managerTest.GetTestById(999, new OptionsTest()));
        }

        [Fact]
        public void GetDisciplinesByStudentId_NotStudent()
        {
            MockHelperUser
                .Setup(x => x.CheckRoleStudent(999))
                .Throws<EducationSystemException>();

            Assert.Throws<EducationSystemException>(
                () => _managerTest.GetTestsForStudent(999, new OptionsTest(), new FilterTest()));
        }

        [Fact]
        public void GetTestsByStudentId_FoundWithThemes()
        {
            MockHelperUser.Reset();

            var tests = GetTests();

            _mockRepositoryTest
                .Setup(x => x.GetTestsForStudent(999, It.IsAny<FilterTest>()))
                .Returns((tests.Count, tests));

            var data = _managerTest.GetTestsForStudent
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