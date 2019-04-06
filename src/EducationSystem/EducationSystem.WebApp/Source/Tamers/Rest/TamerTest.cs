using System.Threading.Tasks;
using EducationSystem.Constants;
using EducationSystem.Interfaces.Managers;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.Models.Rest;
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

        [Transaction]
        [HttpPost("")]
        public async Task<IActionResult> CreateTest([FromBody] Test test)
            => Ok(await _managerTest.CreateTestAsync(test));

        [HttpGet("{testId:int}")]
        public IActionResult GetTest(
            [FromRoute] int testId,
            [FromQuery] OptionsTest options)
            => Ok(_managerTest.GetTestById(testId, options));

        [Transaction]
        [HttpPut("{testId:int}")]
        public async Task<IActionResult> UpdateTest(
            [FromRoute] int testId,
            [FromBody] Test test)
            => Ok(await _managerTest.UpdateTestAsync(testId, test));

        [HttpGet("{testId:int}/Themes")]
        public IActionResult GetTestThemes(
            [FromRoute] int testId,
            [FromQuery] OptionsTheme options,
            [FromQuery] FilterTheme filter)
            => Ok(_managerTheme.GetThemesByTestId(testId, options, filter));

        [Transaction]
        [HttpDelete("{testId:int}")]
        public IActionResult DeleteTest([FromRoute] int testId) =>
            Ok(async () => await _managerTest.DeleteTestByIdAsync(testId));
    }
}