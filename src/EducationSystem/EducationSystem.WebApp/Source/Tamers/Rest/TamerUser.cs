using EducationSystem.Constants.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Rest
{
    [Route("api/Users")]
    public class TamerUser : Controller
    {
        protected IManagerUser ManagerUser { get; }

        public TamerUser(IManagerUser managerUser)
        {
            ManagerUser = managerUser;
        }

        [HttpGet]
        [Route("all")]
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult GetAll()
        {
            return Json(ManagerUser.GetAll());
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult GetById(int id)
        {
            return Json(ManagerUser.GetById(id));
        }
    }
}