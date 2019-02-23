using EducationSystem.Constants.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Models.Source.Options;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Rest
{
    [Route("api/TestResults")]
    [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
    public class TamerTestResults : Tamer
    {
        protected IManagerTestResult ManagerTestResult { get; }

        public TamerTestResults(IManagerTestResult managerTestResult)
        {
            ManagerTestResult = managerTestResult;
        }

        [HttpGet("")]
        public IActionResult GetTestResults(
            [FromQuery] OptionsTestResult options,
            [FromQuery] Filter filter)
            => Ok(ManagerTestResult.GetTests(options, filter));

        [HttpGet("{testResultId:int}")]
        public IActionResult GetTestResult(
            [FromRoute] int testResultId,
            [FromQuery] OptionsTestResult options)
            => Ok(ManagerTestResult.GetTestResultById(testResultId, options));
    }
}