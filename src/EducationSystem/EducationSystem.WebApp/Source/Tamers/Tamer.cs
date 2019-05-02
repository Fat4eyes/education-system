using System;
using System.Threading.Tasks;
using EducationSystem.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers
{
    [ApiController]
    public class Tamer : ControllerBase
    {
        protected new IActionResult Ok(object @object)
            => base.Ok(CreateResponse(@object));

        protected new IActionResult Ok()
            => base.Ok(CreateResponse(null));

        protected async Task<IActionResult> Ok(Func<Task> action)
        {
            await action();

            return Ok();
        }

        protected async Task<IActionResult> Ok<T>(Func<Task<T>> action)
        {
            var response = await action();

            return Ok(response);
        }

        private static SuccessResponse CreateResponse(object @object)
            => new SuccessResponse(@object);
    }
}