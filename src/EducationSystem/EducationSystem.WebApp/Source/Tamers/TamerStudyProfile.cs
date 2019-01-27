using EducationSystem.Managers.Interfaces.Source.Rest;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers
{
    [Route("api/StudyProfiles")]
    public class TamerStudyProfile : Controller
    {
        protected IManagerStudyProfile ManagerStudyProfile { get; }

        public TamerStudyProfile(IManagerStudyProfile managerStudyProfile)
        {
            ManagerStudyProfile = managerStudyProfile;
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAll()
        {
            return Json(ManagerStudyProfile.GetAll());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            return Json(ManagerStudyProfile.GetById(id));
        }
    }
}