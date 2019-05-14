using EducationSystem.Extensions;
using EducationSystem.Interfaces;
using EducationSystem.Models.Rest;

namespace EducationSystem.Resolvers
{
    public abstract class Resolver
    {
        protected readonly IContext Context;

        protected readonly User CurrentUser;

        protected Resolver(IContext context)
        {
            Context = context;

            CurrentUser = Context
                .GetCurrentUserAsync()
                .WaitTask();
        }
    }
}