using EducationSystem.Managers.Interfaces.Source;
using EducationSystem.Models.Source;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Controllers
{
    [Route("api/Auth")]
    public class AuthController : BaseController
    {
        protected IAuthManager AuthManager { get; }

        public AuthController(IAuthManager authManager)
        {
            AuthManager = authManager;
        }

        [HttpPost("signin")]
        public IActionResult SignIn([FromBody] SignInRequest model)
        {
            return Json(AuthManager.SignIn(model));
        }

        [Authorize]
        [HttpPost("check")]
        public IActionResult Check()
        {
            return Ok();
        }
    }
}