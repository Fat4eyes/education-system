using EducationSystem.Constants.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source.Options;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Rest
{
    [Route("api/TestResults")]
    public class TamerTestResults : Tamer
    {
        protected IManagerTestResult ManagerTestResult { get; }

        public TamerTestResults(IManagerTestResult managerTestResult)
        {
            ManagerTestResult = managerTestResult;
        }

        [HttpGet("")]
        [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetTestResults(OptionsTestResult options)
        {
            return Json(ManagerTestResult.GetTests(options));
        }

        [HttpGet("{testResultId:int}")]
        [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
        public IActionResult GetTestResult(int testResultId, OptionsTestResult options)
        {
            return Json(ManagerTestResult.GetTestResultById(testResultId, options));
        }
    }
}