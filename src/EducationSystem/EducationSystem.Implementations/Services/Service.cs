using AutoMapper;
using EducationSystem.Extensions;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Models.Rest;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Services
{
    public abstract class Service<TService> where TService : class
    {
        protected IMapper Mapper { get; }
        protected ILogger<TService> Logger { get; }
        protected IExecutionContext ExecutionContext { get; }
        protected IExceptionFactory ExceptionFactory { get; }

        protected User CurrentUser { get; }

        protected Service(
            IMapper mapper,
            ILogger<TService> logger,
            IExecutionContext executionContext,
            IExceptionFactory exceptionFactory)
        {
            Mapper = mapper;
            Logger = logger;
            ExecutionContext = executionContext;
            ExceptionFactory = exceptionFactory;

            CurrentUser = ExecutionContext
                .GetCurrentUserAsync()
                .WaitTask();
        }
    }
}