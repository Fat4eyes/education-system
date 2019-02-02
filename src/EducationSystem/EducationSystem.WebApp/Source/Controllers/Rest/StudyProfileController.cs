using EducationSystem.Constants.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Controllers.Rest
{
    [Route("api/StudyProfiles")]
    public class StudyProfileController : BaseController
    {
        protected IStudyProfileManager StudyProfileManager { get; }

        public StudyProfileController(IStudyProfileManager studyProfileManager)
        {
            StudyProfileManager = studyProfileManager;
        }

        [HttpGet]
        [Route("all")]
        [Roles(
            UserRoles.Admin,
            UserRoles.Lecturer,
            UserRoles.Employee)]
        public IActionResult GetAll()
        {
            return Json(StudyProfileManager.GetAll());
        }

        [HttpGet]
        [Route("{id:int}")]
        [Roles(
            UserRoles.Admin,
            UserRoles.Lecturer,
            UserRoles.Employee)]
        public IActionResult GetById(int id)
        {
            return Json(StudyProfileManager.GetById(id));
        }
    }
}