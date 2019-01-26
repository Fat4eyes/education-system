using EducationSystem.Managers.Interfaces.Source.Rest;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source
{
    [Route("users")]
    public class TamerUser : Controller
    {
        protected IManagerUser ManagerUser { get; }

        public TamerUser(IManagerUser managerUser)
        {
            ManagerUser = managerUser;
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAllUsers()
        {
            return Json(ManagerUser.GetAllUsers());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetAllUsers(int id)
        {
            return Json(ManagerUser.GetById(id));
        }
    }
}