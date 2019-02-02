using EducationSystem.Constants.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Controllers.Rest
{
    [Route("api/Institutes")]
    public class InstituteController : BaseController
    {
        protected IInstituteManager InstituteManager { get; }

        public InstituteController(IInstituteManager instituteManager)
        {
            InstituteManager = instituteManager;
        }

        [HttpGet]
        [Route("all")]
        [Roles(
            UserRoles.Admin,
            UserRoles.Lecturer,
            UserRoles.Employee)]
        public IActionResult GetAll()
        {
            return Json(InstituteManager.GetAll());
        }

        [HttpGet]
        [Route("{id:int}")]
        [Roles(
            UserRoles.Admin,
            UserRoles.Lecturer,
            UserRoles.Employee)]
        public IActionResult GetById(int id)
        {
            return Json(InstituteManager.GetById(id));
        }
    }
}