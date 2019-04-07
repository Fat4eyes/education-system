using System.Collections.Generic;
using EducationSystem.Database.Models;
using EducationSystem.Exceptions;
using EducationSystem.Implementations.Managers;
using EducationSystem.Interfaces.Managers;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.Repositories.Interfaces;
using Moq;
using Xunit;

namespace EducationSystem.Tests.Managers.Rest
{
    public class TestsManagerDiscipline : TestsManager<ManagerDiscipline>
    {
        private readonly IManagerDiscipline _managerDiscipline;

        private readonly Mock<IRepositoryDiscipline> _mockRepositoryDiscipline
            = new Mock<IRepositoryDiscipline>();

        public TestsManagerDiscipline()
        {
            _managerDiscipline = new ManagerDiscipline(
                Mapper,
                LoggerMock.Object,
                MockHelperUser.Object,
                _mockRepositoryDiscipline.Object);
        }

        [Fact]
        public void GetDisciplineById_Found()
        {
            _mockRepositoryDiscipline
                .Setup(x => x.GetById(999))
                .Returns(new DatabaseDiscipline { Name = "WEB" });

            var discipline = _managerDiscipline.GetDisciplineById(999, new OptionsDiscipline());

            Assert.Equal("WEB", discipline.Name);
        }

        [Fact]
        public void GetDisciplineById_NotFound()
        {
            _mockRepositoryDiscipline
                .Setup(x => x.GetById(999))
                .Returns((DatabaseDiscipline) null);

            Assert.Throws<EducationSystemNotFoundException>(
                () => _managerDiscipline.GetDisciplineById(999, new OptionsDiscipline()));
        }

        [Fact]
        public void GetDisciplinesByStudentId_NotStudent()
        {
            MockHelperUser
                .Setup(x => x.CheckRoleStudent(999))
                .Throws<EducationSystemException>();

            Assert.Throws<EducationSystemException>(
                () => _managerDiscipline.GetDisciplinesForStudent
                    (999, new OptionsDiscipline(), new FilterDiscipline()));
        }

        [Fact]
        public void GetDisciplinesByStudentId_FoundWithTests()
        {
            MockHelperUser.Reset();

            var disciplines = GetDisciplines();

            _mockRepositoryDiscipline
                .Setup(x => x.GetDisciplinesForStudent(999, It.IsAny<FilterDiscipline>()))
                .Returns((disciplines.Count, disciplines));

            var data = _managerDiscipline.GetDisciplinesForStudent
                (999, new OptionsDiscipline { WithTests = true }, new FilterDiscipline());

            Assert.Equal(2, data.Count);

            Assert.Null(data.Items[0].Themes);
            Assert.Null(data.Items[1].Themes);

            Assert.Single(data.Items[0].Tests);
            Assert.Single(data.Items[1].Tests);

            Assert.Equal("Test", data.Items[0].Tests[0].Subject);
            Assert.Equal("Test", data.Items[1].Tests[0].Subject);
        }

        [Fact]
        public void GetDisciplinesByStudentId_FoundWithThemes()
        {
            MockHelperUser.Reset();

            var disciplines = GetDisciplines();

            _mockRepositoryDiscipline
                .Setup(x => x.GetDisciplinesForStudent(999, It.IsAny<FilterDiscipline>()))
                .Returns((disciplines.Count, disciplines));

            var data = _managerDiscipline.GetDisciplinesForStudent
                (999, new OptionsDiscipline { WithThemes = true }, new FilterDiscipline());

            Assert.Equal(2, data.Count);

            Assert.Null(data.Items[0].Tests);
            Assert.Null(data.Items[1].Tests);

            Assert.Single(data.Items[0].Themes);

            Assert.Equal(2, data.Items[1].Themes.Count);

            Assert.Equal("Theme", data.Items[0].Themes[0].Name);
            Assert.Equal("Theme", data.Items[1].Themes[0].Name);
        }

        private static List<DatabaseDiscipline> GetDisciplines()
        {
            return new List<DatabaseDiscipline>
            {
                new DatabaseDiscipline
                {
                    Themes = new List<DatabaseTheme> {
                        Creator.CreateTheme(
                            new DatabaseQuestion(),
                            new DatabaseQuestion(),
                            new DatabaseQuestion())
                    },
                    Tests = new List<DatabaseTest> {
                        Creator.CreateActiveTest(
                            Creator.CreateTheme(
                                new DatabaseQuestion(),
                                new DatabaseQuestion(),
                                new DatabaseQuestion()))
                    }
                },
                new DatabaseDiscipline
                {
                    Themes = new List<DatabaseTheme> {
                        Creator.CreateTheme(
                            new DatabaseQuestion(),
                            new DatabaseQuestion(),
                            new DatabaseQuestion()),
                        Creator.CreateTheme(
                            new DatabaseQuestion(),
                            new DatabaseQuestion(),
                            new DatabaseQuestion())
                    },
                    Tests = new List<DatabaseTest> {
                        Creator.CreateActiveTest(
                            Creator.CreateTheme(
                                new DatabaseQuestion(),
                                new DatabaseQuestion(),
                                new DatabaseQuestion())),
                        Creator.CreateTest(
                            Creator.CreateTheme(
                                new DatabaseQuestion(),
                                new DatabaseQuestion(),
                                new DatabaseQuestion()))
                    }
                }
            };
        }
    }
}