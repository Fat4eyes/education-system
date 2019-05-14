using AutoMapper;
using EducationSystem.Interfaces;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Services
{
    public abstract class Service<TService> where TService : class
    {
        protected readonly IMapper Mapper;
        protected readonly IContext Context;
        protected readonly ILogger<TService> Logger;

        protected Service(IMapper mapper, IContext context, ILogger<TService> logger)
        {
            Mapper = mapper;
            Logger = logger;
            Context = context;
        }
    }
}