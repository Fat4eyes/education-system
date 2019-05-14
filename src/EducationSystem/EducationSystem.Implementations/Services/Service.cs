using AutoMapper;
using EducationSystem.Interfaces;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Services
{
    public abstract class Service<TService> where TService : class
    {
        protected readonly IMapper Mapper;
        protected readonly ILogger<TService> Logger;
        protected readonly IExecutionContext ExecutionContext;

        protected Service(
            IMapper mapper,
            ILogger<TService> logger,
            IExecutionContext executionContext)
        {
            Mapper = mapper;
            Logger = logger;
            ExecutionContext = executionContext;
        }
    }
}