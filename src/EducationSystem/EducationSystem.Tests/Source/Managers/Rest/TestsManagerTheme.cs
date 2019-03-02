using EducationSystem.Database.Models.Source;
using EducationSystem.Exceptions.Source;
using EducationSystem.Helpers.Interfaces.Source;
using EducationSystem.Managers.Implementations.Source.Rest;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source.Options;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Moq;
using Xunit;

namespace EducationSystem.Tests.Source.Managers.Rest
{
    public class TestsManagerTheme : TestsManager<ManagerTheme>
    {
        protected IManagerTheme ManagerTheme { get; }

        protected Mock<IHelperTheme> MockHelperTheme { get; }

        protected Mock<IRepositoryTheme> MockRepositoryTheme { get; }

        public TestsManagerTheme()
        {
            MockHelperTheme = new Mock<IHelperTheme>();

            MockRepositoryTheme = new Mock<IRepositoryTheme>();

            ManagerTheme = new ManagerTheme(
                Mapper,
                LoggerMock.Object,
                MockHelperTheme.Object,
                MockRepositoryTheme.Object);
        }

        [Fact]
        public void GetThemeById_Found()
        {
            MockRepositoryTheme
                .Setup(x => x.GetById(999))
                .Returns(new DatabaseTheme { Name = "HTML" });

            var theme = ManagerTheme.GetThemeById(999, new OptionsTheme());

            Assert.Equal("HTML", theme.Name);
        }

        [Fact]
        public void GetThemeById_NotFound()
        {
            MockRepositoryTheme
                .Setup(x => x.GetById(999))
                .Returns((DatabaseTheme) null);

            Assert.Throws<EducationSystemNotFoundException>(
                () => ManagerTheme.GetThemeById(999, new OptionsTheme()));
        }
    }
}