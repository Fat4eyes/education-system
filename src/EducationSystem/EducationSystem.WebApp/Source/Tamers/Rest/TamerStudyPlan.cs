using EducationSystem.Constants.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Rest
{
    [Route("api/StudyPlans")]
    public class TamerStudyPlan : TamerBase
    {
        protected IManagerStudyPlan ManagerStudyPlan { get; }

        public TamerStudyPlan(IManagerStudyPlan managerStudyPlan)
        {
            ManagerStudyPlan = managerStudyPlan;
        }

        [HttpGet]
        [Route("all")]
        [Roles(
            UserRoles.Admin,
            UserRoles.Lecturer,
            UserRoles.Employee)]
        public IActionResult GetAll()
        {
            return Json(ManagerStudyPlan.GetAll());
        }

        [HttpGet]
        [Route("{id:int}")]
        [Roles(
            UserRoles.Admin,
            UserRoles.Lecturer,
            UserRoles.Employee)]
        public IActionResult GetById(int id)
        {
            return Json(ManagerStudyPlan.GetById(id));
        }
    }
}