using EducationSystem.Extensions;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Models.Rest;

namespace EducationSystem.Implementations.Managers
{
    public abstract class Manager
    {
        protected IExecutionContext ExecutionContext { get; }
        protected IExceptionFactory ExceptionFactory { get; }

        protected User CurrentUser { get; }

        protected Manager(IExecutionContext executionContext, IExceptionFactory exceptionFactory)
        {
            ExecutionContext = executionContext;
            ExceptionFactory = exceptionFactory;

            CurrentUser = ExecutionContext
                .GetCurrentUserAsync()
                .WaitTask();
        }
    }
}