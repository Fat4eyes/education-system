using System.Threading.Tasks;
using EducationSystem.Constants;
using EducationSystem.Interfaces.Managers;
using EducationSystem.Models.Filters;
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
        [Roles(UserRoles.Admin, UserRoles.Lecturer, UserRoles.Student)]
        public async Task<IActionResult> GetTests([FromQuery] FilterTest filter)
        {
            return await Ok(() => _managerTest.GetTestsAsync(filter));
        }

        [HttpGet("{id:int}")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer, UserRoles.Student)]
        public async Task<IActionResult> GetTest([FromRoute] int id)
        {
            return await Ok(() => _managerTest.GetTestAsync(id));
        }

        [HttpPost]
        [Transaction]
        [Roles(UserRoles.Lecturer)]
        public async Task<IActionResult> CreateTest([FromBody] Test test)
        {
            return await Ok(() => _managerTest.CreateTestAsync(test));
        }

        [Transaction]
        [HttpPut("{id:int}")]
        [Roles(UserRoles.Lecturer)]
        public async Task<IActionResult> UpdateTest([FromRoute] int id, [FromBody] Test test)
        {
            return await Ok(() => _managerTest.UpdateTestAsync(id, test));
        }

        [Transaction]
        [HttpDelete("{id:int}")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> DeleteTest([FromRoute] int id)
        {
            return await Ok(() => _managerTest.DeleteTestAsync(id));
        }

        [HttpGet("{id:int}/Themes")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> GetTestThemes([FromRoute] int id, [FromQuery] FilterTheme filter)
        {
            return await Ok(() => _managerTheme.GetThemesAsync(filter.SetTestId(id)));
        }
    }
}