using System;
using System.Net;
using System.Threading.Tasks;
using EducationSystem.Constants.Source;
using EducationSystem.Exceptions.Source;
using EducationSystem.Models.Source.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EducationSystem.WebApp.Source.Handlers
{
    public class ExceptionHandler
    {
        protected RequestDelegate Next { get; }

        public ExceptionHandler(RequestDelegate next)
        {
            Next = next;
        }

        public async Task Invoke(HttpContext context, ILogger<ExceptionHandler> logger)
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
                case EducationSystemPublicException _:
                    return CreateSuccessResponse(context, exception);

                case EducationSystemNotFoundException ex:
                    return ex.InnerException is EducationSystemPublicException
                        ? CreateSuccessResponse(context, ex.InnerException)
                        : CreateErrorResponse(context);
            }

            return CreateErrorResponse(context);
        }

        private static Task CreateSuccessResponse(HttpContext context, Exception exception) =>
            CreateResponse(context, HttpStatusCode.OK,
                JsonConvert.SerializeObject(new ErrorResponse(exception.Message)));

        private static Task CreateErrorResponse(HttpContext context) =>
            CreateResponse(context, HttpStatusCode.InternalServerError, Messages.InternalServerError);

        private static Task CreateResponse(HttpContext context, HttpStatusCode statusCode, string text)
        {
            var response = context.Response;

            response.ContentType = "application/json";
            response.StatusCode = (int) statusCode;

            return response.WriteAsync(text);
        }
    }
}