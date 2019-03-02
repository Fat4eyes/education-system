using EducationSystem.Constants.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Rest
{
    [Route("api/Tests")]
    [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
    public class TamerTest : Tamer
    {
        private readonly IManagerTest _managerTest;
        private readonly IManagerTheme _managerTheme;

        public TamerTest(IManagerTest managerTest, IManagerTheme managerTheme)
        {
            _managerTest = managerTest;
            _managerTheme = managerTheme;
        }

        [HttpGet("")]
        public IActionResult GetTests(
            [FromQuery] OptionsTest options,
            [FromQuery] FilterTest filter)
            => Ok(_managerTest.GetTests(options, filter));

        [HttpPost("")]
        public IActionResult CreateTest([FromBody] Test test)
            => Ok(_managerTest.CreateTest(test));

        [HttpGet("{testId:int}")]
        public IActionResult GetTest(
            [FromRoute] int testId,
            [FromQuery] OptionsTest options)
            => Ok(_managerTest.GetTestById(testId, options));

        [HttpGet("{testId:int}/Themes")]
        public IActionResult GetTestThemes(
            [FromRoute] int testId,
            [FromQuery] OptionsTheme options,
            [FromQuery] FilterTheme filter)
            => Ok(_managerTheme.GetThemesByTestId(testId, options, filter));

        [HttpDelete("{testId:int}")]
        public IActionResult DeleteTest([FromRoute] int testId) =>
            Ok(() => _managerTest.DeleteTestById(testId));
    }
}