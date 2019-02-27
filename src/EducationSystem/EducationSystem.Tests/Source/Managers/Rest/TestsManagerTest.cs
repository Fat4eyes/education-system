using System.Collections.Generic;
using EducationSystem.Database.Models.Source;
using EducationSystem.Exceptions.Source;
using EducationSystem.Managers.Implementations.Source.Rest;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Models.Source.Options;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Moq;
using Xunit;

namespace EducationSystem.Tests.Source.Managers.Rest
{
    public class TestsManagerTest : TestsManager<ManagerTest>
    {
        protected IManagerTest ManagerTest { get; }

        protected Mock<IRepositoryTest> MockRepositoryTest { get; set; }

        public TestsManagerTest()
        {
            MockRepositoryTest = new Mock<IRepositoryTest>();

            ManagerTest = new ManagerTest(
                Mapper,
                LoggerMock.Object,
                MockUserHelper.Object,
                MockRepositoryTest.Object);
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
            MockUserHelper
                .Setup(x => x.IsStudent(999))
                .Returns(false);

            Assert.Throws<EducationSystemException>(
                () => ManagerTest.GetTestsByStudentId(999, new OptionsTest(), new FilterTest()));
        }

        [Fact]
        public void GetTestsByStudentId_FoundWithThemes()
        {
            MockUserHelper
                .Setup(x => x.IsStudent(999))
                .Returns(true);

            var tests = GetTests();

            MockRepositoryTest
                .Setup(x => x.GetTestsByStudentId(999, It.IsAny<FilterTest>()))
                .Returns((tests.Count, tests));

            var data = ManagerTest.GetTestsByStudentId
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
                new DatabaseTest
                {
                    TestThemes = new List<DatabaseTestTheme>
                    {
                        new DatabaseTestTheme { Theme = Creator.CreateThemeWithQuestions() },
                        new DatabaseTestTheme { Theme = Creator.CreateThemeWithQuestions() }
                    }
                },
                new DatabaseTest
                {
                    TestThemes = new List<DatabaseTestTheme>
                    {
                        new DatabaseTestTheme { Theme = Creator.CreateThemeWithQuestions() },
                        new DatabaseTestTheme { Theme = Creator.CreateTheme() }
                    }
                },
                new DatabaseTest
                {
                    TestThemes = new List<DatabaseTestTheme>
                    {
                        new DatabaseTestTheme { Theme = Creator.CreateThemeWithQuestions() },
                        new DatabaseTestTheme { Theme = Creator.CreateTheme() },
                        new DatabaseTestTheme { Theme = Creator.CreateTheme() }
                    }
                }
            };
        }
    }
}