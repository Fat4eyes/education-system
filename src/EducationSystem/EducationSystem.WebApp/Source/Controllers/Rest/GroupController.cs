using EducationSystem.Constants.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Controllers.Rest
{
    [Route("api/Groups")]
    public class GroupController : BaseController
    {
        protected IGroupManager GroupManager { get; }

        public GroupController(IGroupManager groupManager)
        {
            GroupManager = groupManager;
        }

        [HttpGet]
        [Route("all")]
        [Roles(
            UserRoles.Admin,
            UserRoles.Lecturer,
            UserRoles.Employee)]
        public IActionResult GetAll()
        {
            return Json(GroupManager.GetAll());
        }

        [HttpGet]
        [Route("{id:int}")]
        [Roles(
            UserRoles.Admin,
            UserRoles.Lecturer,
            UserRoles.Employee)]
        public IActionResult GetById(int id)
        {
            return Json(GroupManager.GetById(id));
        }
    }
}