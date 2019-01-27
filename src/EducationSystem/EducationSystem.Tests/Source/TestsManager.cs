using Moq;
using AutoMapper;
using EducationSystem.Mapping.Source;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Tests.Source
{
    public abstract class TestsManager<TManager> where TManager : class
    {
        protected IMapper Mapper { get; }

        protected Mock<ILogger<TManager>> LoggerMock { get; }

        protected TestsManager()
        {
            Mapper = new Mapper(new MapperConfiguration(MappingConfigurator.Configure));

            LoggerMock = new Mock<ILogger<TManager>>();
        }
    }
}