using System.Threading.Tasks;
using EducationSystem.Database.Models;
using EducationSystem.Exceptions;
using EducationSystem.Implementations.Managers;
using EducationSystem.Interfaces.Managers;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models.Options;
using EducationSystem.Models.Rest;
using EducationSystem.Repositories.Interfaces;
using Moq;
using Xunit;

namespace EducationSystem.Tests.Managers.Rest
{
    public class TestsManagerTheme : TestsManager<ManagerTheme>
    {
        private readonly IManagerTheme _managerTheme;

        private readonly Mock<IValidator<Theme>> _mockCheckerTheme
            = new Mock<IValidator<Theme>>();

        private readonly Mock<IRepositoryTheme> _mockRepositoryTheme
            = new Mock<IRepositoryTheme>();

        private readonly Mock<IRepositoryDiscipline> _mockRepositoryDiscipline
            = new Mock<IRepositoryDiscipline>();

        public TestsManagerTheme()
        {
            _managerTheme = new ManagerTheme(
                Mapper,
                LoggerMock.Object,
                _mockCheckerTheme.Object,
                _mockRepositoryTheme.Object,
                _mockRepositoryDiscipline.Object);
        }

        [Fact]
        public async Task GetTest_Found()
        {
            _mockRepositoryTheme
                .Setup(x => x.GetById(999))
                .ReturnsAsync(new DatabaseTheme { Name = "HTML" });

            var theme = await _managerTheme.GetTheme(999, new OptionsTheme());

            Assert.Equal("HTML", theme.Name);
        }

        [Fact]
        public async Task GetTest_NotFound()
        {
            _mockRepositoryTheme
                .Setup(x => x.GetById(999))
                .ReturnsAsync((DatabaseTheme) null);

            await Assert.ThrowsAsync<EducationSystemNotFoundException>(
                () => _managerTheme.GetTheme(999, new OptionsTheme()));
        }
    }
}