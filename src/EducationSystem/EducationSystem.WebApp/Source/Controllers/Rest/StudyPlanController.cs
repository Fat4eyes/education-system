using EducationSystem.Constants.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Controllers.Rest
{
    [Route("api/StudyPlans")]
    public class StudyPlanController : BaseController
    {
        protected IStudyPlanManager StudyPlanManager { get; }

        public StudyPlanController(IStudyPlanManager studyPlanManager)
        {
            StudyPlanManager = studyPlanManager;
        }

        [HttpGet]
        [Route("all")]
        [Roles(
            UserRoles.Admin,
            UserRoles.Lecturer,
            UserRoles.Employee)]
        public IActionResult GetAll()
        {
            return Json(StudyPlanManager.GetAll());
        }

        [HttpGet]
        [Route("{id:int}")]
        [Roles(
            UserRoles.Admin,
            UserRoles.Lecturer,
            UserRoles.Employee)]
        public IActionResult GetById(int id)
        {
            return Json(StudyPlanManager.GetById(id));
        }
    }
}