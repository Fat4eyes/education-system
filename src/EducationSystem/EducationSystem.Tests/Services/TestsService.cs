using AutoMapper;
using EducationSystem.Interfaces;
using EducationSystem.Mapping;
using Microsoft.Extensions.Logging;
using Moq;

namespace EducationSystem.Tests.Services
{
    public abstract class TestsService<TService>
    {
        protected readonly IMapper Mapper = new Mapper(new MapperConfiguration(MappingConfigurator.Configure));

        protected readonly Mock<IContext> Context = new Mock<IContext>();

        protected readonly Mock<ILogger<TService>> Logger = new Mock<ILogger<TService>>();
    }
}