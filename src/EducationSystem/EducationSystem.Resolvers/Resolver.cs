using EducationSystem.Extensions;
using EducationSystem.Interfaces;
using EducationSystem.Models.Rest;

namespace EducationSystem.Resolvers
{
    public abstract class Resolver
    {
        protected IExecutionContext ExecutionContext { get; }

        protected User CurrentUser { get; }

        protected Resolver(IExecutionContext executionContext)
        {
            ExecutionContext = executionContext;

            CurrentUser = ExecutionContext
                .GetCurrentUserAsync()
                .WaitTask();
        }
    }
}