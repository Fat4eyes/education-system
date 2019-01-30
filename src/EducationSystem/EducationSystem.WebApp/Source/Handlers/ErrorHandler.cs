using System;
using System.Net;
using System.Threading.Tasks;
using EducationSystem.Exceptions.Source;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace EducationSystem.WebApp.Source.Handlers
{
    /// <summary>
    /// Обработчик ошибок (промежуточный слой).
    /// </summary>
    public class ErrorHandler
    {
        protected RequestDelegate Next { get; }

        public ErrorHandler(RequestDelegate next)
        {
            Next = next;
        }

        public async Task Invoke(HttpContext context, ILogger<ErrorHandler> logger)
        {
            try
            {
                await Next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(logger, context, ex);
            }
        }

        private static Task HandleExceptionAsync(ILogger logger, HttpContext context, Exception exception)
        {
            logger.LogError(exception, exception.Message);

            switch (exception)
            {
                case EducationSystemException _:
                    return CreateResponse(context, HttpStatusCode.InternalServerError, exception.Message);
                case EducationSystemNotFoundException _:
                    return CreateResponse(context, HttpStatusCode.NotFound, exception.Message);
            }

            return CreateResponse(context, HttpStatusCode.InternalServerError, exception.Message);
        }

        private static Task CreateResponse(HttpContext context, HttpStatusCode statusCode, string text)
        {
            var response = context.Response;

            response.ContentType = "application/json";
            response.StatusCode = (int) statusCode;

            return response.WriteAsync(text);
        }
    }
}