using System;
using System.Linq;
using EducationSystem.Models;
using EducationSystem.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EducationSystem.WebApp.Source.Tamers
{
    [ApiController]
    [JsonConverter(typeof(StringEnumConverter))]
    public class Tamer : ControllerBase
    {
        protected int GetUserId()
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value
                ?? throw new ApplicationException("Не удалось получить идентификатор пользователя.");

            return Convert.ToInt32(userId);
        }

        protected new IActionResult Ok(object @object)
            => base.Ok(CreateResponse(@object));

        protected new IActionResult Ok()
            => base.Ok(CreateResponse(null));

        protected IActionResult Ok(Action action)
        {
            action();
            return Ok();
        }

        protected IActionResult File(File file) =>
            File(file.Stream, "application/octet-stream", file.Name);

        private static SuccessResponse CreateResponse(object @object)
            => new SuccessResponse(@object);
    }
}