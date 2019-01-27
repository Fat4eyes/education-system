using AutoMapper;
using EducationSystem.Mapping.Source;

namespace EducationSystem.Tests.Source
{
    public abstract class TestsManager
    {
        protected IMapper Mapper { get; }

        protected TestsManager()
        {
            Mapper = new Mapper(new MapperConfiguration(MappingConfigurator.Configure));
        }
    }
}