using EducationSystem.Constants.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Models.Source.Options;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Rest
{
    [Route("api/Tests")]
    [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
    public class TamerTest : Tamer
    {
        protected IManagerTest ManagerTest { get; }
        protected IManagerTheme ManagerTheme { get; }

        public TamerTest(IManagerTest managerTest, IManagerTheme managerTheme)
        {
            ManagerTest = managerTest;
            ManagerTheme = managerTheme;
        }

        [HttpGet("")]
        public IActionResult GetTests(
            [FromQuery] OptionsTest options,
            [FromQuery] FilterTest filter)
            => Ok(ManagerTest.GetTests(options, filter));

        [HttpGet("{testId:int}")]
        public IActionResult GetTest(
            [FromRoute] int testId,
            [FromQuery] OptionsTest options)
            => Ok(ManagerTest.GetTestById(testId, options));

        [HttpGet("{testId:int}/Themes")]
        public IActionResult GetTestThemes(
            [FromRoute] int testId,
            [FromQuery] OptionsTheme options,
            [FromQuery] FilterTheme filter)
            => Ok(ManagerTheme.GetThemesByTestId(testId, options, filter));

        [HttpDelete("{testId:int}")]
        public IActionResult DeleteTest([FromRoute] int testId) =>
            Ok(() => ManagerTest.DeleteTestById(testId));
    }
}