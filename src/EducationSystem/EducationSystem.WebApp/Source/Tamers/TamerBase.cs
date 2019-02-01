using EducationSystem.Models.Source.Responses;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers
{
    public class TamerBase : Controller
    {
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