using EducationSystem.Constants.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Controllers.Rest
{
    [Route("api/Users")]
    public class UserController : BaseController
    {
        protected IUserManager UserManager { get; }

        public UserController(IUserManager userManager)
        {
            UserManager = userManager;
        }

        [HttpGet]
        [Route("all")]
        [Roles(UserRoles.Admin)]
        public IActionResult GetAll()
        {
            return Json(UserManager.GetAll());
        }

        [HttpGet]
        [Route("{id:int}")]
        [Roles(UserRoles.Admin)]
        public IActionResult GetById(int id)
        {
            return Json(UserManager.GetById(id));
        }
    }
}