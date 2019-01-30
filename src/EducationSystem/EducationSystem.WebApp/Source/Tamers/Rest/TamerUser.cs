using EducationSystem.Constants.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.WebApp.Source.Attributes;
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
        [Roles(UserRoles.Admin)]
        public IActionResult GetAll()
        {
            return Json(ManagerUser.GetAll());
        }

        [HttpGet]
        [Route("{id:int}")]
        [Roles(UserRoles.Admin)]
        public IActionResult GetById(int id)
        {
            return Json(ManagerUser.GetById(id));
        }
    }
}