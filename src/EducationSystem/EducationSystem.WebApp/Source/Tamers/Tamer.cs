using System;
using System.Linq;
using EducationSystem.Models.Source.Responses;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers
{
    public class Tamer : Controller
    {
        protected int GetUserId()
        {
            var userId = HttpContext.User?.Claims?
                .FirstOrDefault(x => x.Type == "UserId")?
                .Value ?? throw new ApplicationException("Не удалось получить идентификатор пользователя.");

            return Convert.ToInt32(userId);
        }

        protected new IActionResult Ok(object @object)
            => base.Json(CreateResponse(@object));

        protected new IActionResult Json(object @object)
            => base.Json(CreateResponse(@object));

        protected IActionResult Json()
            => base.Json(CreateResponse(null));

        protected new IActionResult Ok()
            => base.Json(CreateResponse(null));

        private static SuccessResponse CreateResponse(object @object)
            => new SuccessResponse(@object);
    }
}