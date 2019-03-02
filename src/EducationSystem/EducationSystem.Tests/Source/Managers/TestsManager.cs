using AutoMapper;
using EducationSystem.Helpers.Interfaces.Source;
using EducationSystem.Mapping.Source;
using Microsoft.Extensions.Logging;
using Moq;

namespace EducationSystem.Tests.Source.Managers
{
    public abstract class TestsManager<TManager> where TManager : class
    {
        protected IMapper Mapper { get; }

        protected Mock<ILogger<TManager>> LoggerMock { get; }

        protected Mock<IHelperUser> MockUserHelper { get; set; }

        protected TestsManager()
        {
            Mapper = new Mapper(new MapperConfiguration(MappingConfigurator.Configure));

            LoggerMock = new Mock<ILogger<TManager>>();

            MockUserHelper = new Mock<IHelperUser>();
        }
    }
}