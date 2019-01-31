using EducationSystem.Managers.Interfaces.Source;
using EducationSystem.Models.Source;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers
{
    [Route("api/Auth")]
    public class TamerAuth : Controller
    {
        protected IAuthManager AuthManager { get; }

        public TamerAuth(IAuthManager authManager)
        {
            AuthManager = authManager;
        }

        [HttpPost("signin")]
        public IActionResult SignIn([FromBody] SignInRequest model)
        {
            return Ok(AuthManager.SignIn(model));
        }

        [HttpPost("check")]
        [Authorize]
        public IActionResult Check()
        {
            return Ok();
        }
    }
}