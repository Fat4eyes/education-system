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
    public class TamerTest : Tamer
    {
        private readonly IManagerTest _managerTest;
        private readonly IManagerTheme _managerTheme;

        public TamerTest(IManagerTest managerTest, IManagerTheme managerTheme)
        {
            _managerTest = managerTest;
            _managerTheme = managerTheme;
        }

        [HttpGet]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> GetTests([FromQuery] OptionsTest options, [FromQuery] FilterTest filter)
        {
            return Ok(await _managerTest.GetTestsAsync(options, filter));
        }

        [Transaction]
        [HttpPost("")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> CreateTest([FromBody] Test test)
        {
            return Ok(await _managerTest.CreateTestAsync(test));
        }

        [HttpGet("{id:int}")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> GetTest([FromRoute] int id, [FromQuery] OptionsTest options)
        {
            return Ok(await _managerTest.GetTestAsync(id, options));
        }

        [Transaction]
        [HttpPut("{id:int}")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> UpdateTest([FromRoute] int id, [FromBody] Test test)
        {
            return Ok(await _managerTest.UpdateTestAsync(id, test));
        }

        [Transaction]
        [HttpDelete("{id:int}")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> DeleteTest([FromRoute] int id)
        {
            await _managerTest.DeleteTestAsync(id);

            return Ok();
        }

        [HttpGet("{id:int}/Themes")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> GetThemesByTestId(
            [FromRoute] int id,
            [FromQuery] OptionsTheme options,
            [FromQuery] FilterTheme filter)
        {
            return Ok(await _managerTheme.GetThemesByTestIdAsync(id, options, filter));
        }
    }
}