using EducationSystem.Constants.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Rest
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
        [Authorize(Roles = UserRoles.AdminAndLecturerAndEmployee)]
        public IActionResult GetAll()
        {
            return Json(ManagerStudyProfile.GetAll());
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize(Roles = UserRoles.AdminAndLecturerAndEmployee)]
        public IActionResult GetById(int id)
        {
            return Json(ManagerStudyProfile.GetById(id));
        }
    }
}