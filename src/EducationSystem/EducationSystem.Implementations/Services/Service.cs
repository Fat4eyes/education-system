using AutoMapper;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Services
{
    public abstract class Service<TService> where TService : class
    {
        protected IMapper Mapper { get; }

        protected ILogger<TService> Logger { get; }

        protected Service(IMapper mapper, ILogger<TService> logger)
        {
            Mapper = mapper;
            Logger = logger;
        }
    }
}