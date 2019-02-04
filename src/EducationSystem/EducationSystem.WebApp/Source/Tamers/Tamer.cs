using System.Linq;
using System.Security.Claims;
using EducationSystem.Models.Source.Responses;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers
{
    public class Tamer : Controller
    {
        protected string GetCurrentUserEmail() =>
            HttpContext.User?.Claims?
                .FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultNameClaimType)?
                .Value;

        protected new IActionResult Ok(object @object)
            => base.Json(CreateResponse(@object));

        protected new IActionResult Json(object @object)
            => base.Json(CreateResponse(@object));

        protected new IActionResult Ok()
            => base.Json(CreateResponse(null));

        private static SuccessResponse CreateResponse(object @object)
            => new SuccessResponse(@object);
    }
}