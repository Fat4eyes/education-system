using System.Collections.Generic;
using System.Threading.Tasks;
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
                MockExceptionFactory.Object,
                _mockRepositoryDiscipline.Object);
        }

        [Fact]
        public async Task GetDiscipline_Found()
        {
            _mockRepositoryDiscipline
                .Setup(x => x.GetByIdAsync(999))
                .ReturnsAsync(new DatabaseDiscipline { Name = "WEB" });

            var discipline = await _managerDiscipline.GetDisciplineAsync(999, new OptionsDiscipline());

            Assert.Equal("WEB", discipline.Name);
        }

        [Fact]
        public async Task GetDiscipline_NotFound()
        {
            _mockRepositoryDiscipline
                .Setup(x => x.GetByIdAsync(999))
                .ReturnsAsync((DatabaseDiscipline) null);

            MockExceptionFactory
                .Setup(x => x.NotFound<DatabaseDiscipline>(It.IsAny<int>()))
                .Returns(new EducationSystemNotFoundException());

            await Assert.ThrowsAsync<EducationSystemNotFoundException>(
                () => _managerDiscipline.GetDisciplineAsync(999, new OptionsDiscipline()));
        }

        [Fact]
        public async Task GetDisciplinesByStudentId_NotStudent()
        {
            MockHelperUser
                .Setup(x => x.CheckRoleStudentAsync(999))
                .Throws<EducationSystemException>();

            await Assert.ThrowsAsync<EducationSystemException>(
                () => _managerDiscipline.GetDisciplinesByStudentIdAsync
                    (999, new OptionsDiscipline(), new FilterDiscipline()));
        }

        [Fact]
        public async Task GetDisciplinesByStudentId_FoundWithTests()
        {
            MockHelperUser.Reset();

            var disciplines = GetDisciplines();

            _mockRepositoryDiscipline
                .Setup(x => x.GetDisciplinesByStudentIdAsync(999, It.IsAny<FilterDiscipline>()))
                .ReturnsAsync((disciplines.Count, disciplines));

            var data = await _managerDiscipline.GetDisciplinesByStudentIdAsync
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
        public async Task GetDisciplinesByStudentId_FoundWithThemes()
        {
            MockHelperUser.Reset();

            var disciplines = GetDisciplines();

            _mockRepositoryDiscipline
                .Setup(x => x.GetDisciplinesByStudentIdAsync(999, It.IsAny<FilterDiscipline>()))
                .ReturnsAsync((disciplines.Count, disciplines));

            var data = await _managerDiscipline.GetDisciplinesByStudentIdAsync
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