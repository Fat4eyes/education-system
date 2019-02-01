using EducationSystem.Managers.Interfaces.Source;
using EducationSystem.Models.Source;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers
{
    [Route("api/Auth")]
    public class TamerAuth : TamerBase
    {
        protected IAuthManager AuthManager { get; }

        public TamerAuth(IAuthManager authManager)
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