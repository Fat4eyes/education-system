using AutoMapper;
using EducationSystem.Extensions;
using EducationSystem.Interfaces;
using EducationSystem.Models.Rest;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Services
{
    public abstract class Service<TService> where TService : class
    {
        protected IMapper Mapper { get; }
        protected ILogger<TService> Logger { get; }
        protected IExecutionContext ExecutionContext { get; }

        protected User CurrentUser { get; }

        protected Service(
            IMapper mapper,
            ILogger<TService> logger,
            IExecutionContext executionContext)
        {
            Mapper = mapper;
            Logger = logger;
            ExecutionContext = executionContext;

            CurrentUser = ExecutionContext
                .GetCurrentUserAsync()
                .WaitTask();
        }
    }
}