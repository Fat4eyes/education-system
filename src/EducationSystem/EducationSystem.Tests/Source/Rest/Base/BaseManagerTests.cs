using AutoMapper;
using EducationSystem.Mapping.Source;
using Microsoft.Extensions.Logging;
using Moq;

namespace EducationSystem.Tests.Source.Rest.Base
{
    public abstract class BaseManagerTests<TManager> where TManager : class
    {
        protected IMapper Mapper { get; }

        protected Mock<ILogger<TManager>> LoggerMock { get; }

        protected BaseManagerTests()
        {
            Mapper = new Mapper(new MapperConfiguration(MappingConfigurator.Configure));

            LoggerMock = new Mock<ILogger<TManager>>();
        }
    }
}