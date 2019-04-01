using AutoMapper;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Managers
{
    public abstract class Manager<TManager> where TManager : class
    {
        protected IMapper Mapper { get; }

        protected ILogger<TManager> Logger { get; }

        protected Manager(IMapper mapper, ILogger<TManager> logger)
        {
            Mapper = mapper;
            Logger = logger;
        }
    }
}