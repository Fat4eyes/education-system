using EducationSystem.Managers.Interfaces.Source.Rest;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers
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
        public IActionResult GetAll()
        {
            return Json(ManagerGroup.GetAll());
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            return Json(ManagerGroup.GetById(id));
        }
    }
}