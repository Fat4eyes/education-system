using EducationSystem.Database.Models;
using EducationSystem.Exceptions;
using EducationSystem.Implementations.Managers.Rest;
using EducationSystem.Interfaces.Managers.Rest;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models.Options;
using EducationSystem.Models.Source.Rest;
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

        public TestsManagerTheme()
        {
            _managerTheme = new ManagerTheme(
                Mapper,
                LoggerMock.Object,
                _mockCheckerTheme.Object,
                _mockRepositoryTheme.Object);
        }

        [Fact]
        public void GetThemeById_Found()
        {
            _mockRepositoryTheme
                .Setup(x => x.GetById(999))
                .Returns(new DatabaseTheme { Name = "HTML" });

            var theme = _managerTheme.GetThemeById(999, new OptionsTheme());

            Assert.Equal("HTML", theme.Name);
        }

        [Fact]
        public void GetThemeById_NotFound()
        {
            _mockRepositoryTheme
                .Setup(x => x.GetById(999))
                .Returns((DatabaseTheme) null);

            Assert.Throws<EducationSystemNotFoundException>(
                () => _managerTheme.GetThemeById(999, new OptionsTheme()));
        }
    }
}