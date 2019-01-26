using AutoMapper;

namespace EducationSystem.Managers.Implementations.Source.Rest
{
    public abstract class Manager
    {
        protected IMapper Mapper { get; }

        protected Manager(IMapper mapper)
        {
            Mapper = mapper;
        }
    }
}