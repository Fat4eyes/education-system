using EducationSystem.Constants.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Rest
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
        [Authorize(Roles = UserRoles.AdminAndLecturerAndEmployee)]
        public IActionResult GetAll()
        {
            return Json(ManagerInstitute.GetAll());
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize(Roles = UserRoles.AdminAndLecturerAndEmployee)]
        public IActionResult GetById(int id)
        {
            return Json(ManagerInstitute.GetById(id));
        }
    }
}