using System;
using System.Linq;
using EducationSystem.Models.Source.Responses;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers
{
    [ApiController]
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

        private static SuccessResponse CreateResponse(object @object)
            => new SuccessResponse(@object);
    }
}