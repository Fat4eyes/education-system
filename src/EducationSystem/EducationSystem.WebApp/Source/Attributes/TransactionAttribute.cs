using System;
using EducationSystem.Database.Source.Contexts;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace EducationSystem.WebApp.Source.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class TransactionAttribute : ActionFilterAttribute
    {
        private IDbContextTransaction _transaction;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var provider = context.HttpContext.RequestServices;

            var database = provider
                .GetService<DatabaseContext>()
                .Database;

            _transaction = database.BeginTransaction();
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                _transaction.Rollback();
            }
            else
            {
                _transaction.Commit();
            }
        }
    }
}