using EducationSystem.Managers.Interfaces.Source.Rest;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers
{
    [Route("api/Institutes")]
    public class TamerInstitute : Controller
    {
        protected IManagerInstitute ManagerInstitute { get; }

        public TamerInstitute(IManagerInstitute managerInstitute)
        {
            ManagerInstitute = managerInstitute;
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAll()
        {
            return Json(ManagerInstitute.GetAll());
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            return Json(ManagerInstitute.GetById(id));
        }
    }
}