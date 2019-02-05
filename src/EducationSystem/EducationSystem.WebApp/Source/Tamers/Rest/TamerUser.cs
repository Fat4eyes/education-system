using EducationSystem.Managers.Interfaces.Source.Rest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Rest
{
    [Route("api/Users")]
    public class TamerUser : Tamer
    {
        protected IManagerUser ManagerUser { get; }

        public TamerUser(IManagerUser managerUser)
        {
            ManagerUser = managerUser;
        }

        [HttpGet]
        [Authorize]
        [Route("current")]
        public IActionResult Current()
        {
            return Json(ManagerUser.GetUserWithGroupByEmail(GetCurrentUserEmail()));
        }
    }
}