using AutoMapper;

namespace EducationSystem.Managers.Implementations.Source.Rest
{
    /// <summary>
    /// Базовый менеджер.
    /// </summary>
    public abstract class Manager
    {
        protected IMapper Mapper { get; }

        protected Manager(IMapper mapper)
        {
            Mapper = mapper;
        }
    }
}