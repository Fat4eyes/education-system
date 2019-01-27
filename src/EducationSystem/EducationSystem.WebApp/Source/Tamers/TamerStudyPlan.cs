using EducationSystem.Managers.Interfaces.Source.Rest;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers
{
    [Route("api/StudyPlans")]
    public class TamerStudyPlan : Controller
    {
        protected IManagerStudyPlan ManagerStudyPlan { get; }

        public TamerStudyPlan(IManagerStudyPlan managerStudyPlan)
        {
            ManagerStudyPlan = managerStudyPlan;
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAll()
        {
            return Json(ManagerStudyPlan.GetAll());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            return Json(ManagerStudyPlan.GetById(id));
        }
    }
}