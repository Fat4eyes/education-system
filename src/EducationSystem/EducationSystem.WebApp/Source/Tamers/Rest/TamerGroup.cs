using EducationSystem.Constants.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Rest
{
    [Route("api/Groups")]
    public class TamerGroup : Controller
    {
        protected IManagerGroup ManagerGroup { get; }

        public TamerGroup(IManagerGroup managerGroup)
        {
            ManagerGroup = managerGroup;
        }

        [HttpGet]
        [Route("all")]
        [Authorize(Roles = UserRoles.AdminAndLecturerAndEmployee)]
        public IActionResult GetAll()
        {
            return Json(ManagerGroup.GetAll());
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize(Roles = UserRoles.AdminAndLecturerAndEmployee)]
        public IActionResult GetById(int id)
        {
            return Json(ManagerGroup.GetById(id));
        }
    }
}