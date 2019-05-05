using System.Threading.Tasks;
using EducationSystem.Constants;
using EducationSystem.Interfaces.Services;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Rest;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Rest
{
    [Route("api/Tests")]
    public class TamerTest : Tamer
    {
        private readonly IServiceTest _serviceTest;
        private readonly IServiceTheme _serviceTheme;

        public TamerTest(IServiceTest serviceTest, IServiceTheme serviceTheme)
        {
            _serviceTest = serviceTest;
            _serviceTheme = serviceTheme;
        }

        [HttpGet]
        [Roles(UserRoles.Admin, UserRoles.Lecturer, UserRoles.Student)]
        public async Task<IActionResult> GetTests([FromQuery] FilterTest filter)
        {
            return await Ok(() => _serviceTest.GetTestsAsync(filter));
        }

        [HttpGet("{id:int}")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer, UserRoles.Student)]
        public async Task<IActionResult> GetTest([FromRoute] int id)
        {
            return await Ok(() => _serviceTest.GetTestAsync(id));
        }

        [HttpPost]
        [Transaction]
        [Roles(UserRoles.Lecturer)]
        public async Task<IActionResult> CreateTest([FromBody] Test test)
        {
            return await Ok(() => _serviceTest.CreateTestAsync(test));
        }

        [Transaction]
        [HttpPut("{id:int}")]
        [Roles(UserRoles.Lecturer)]
        public async Task<IActionResult> UpdateTest([FromRoute] int id, [FromBody] Test test)
        {
            return await Ok(() => _serviceTest.UpdateTestAsync(id, test));
        }

        [Transaction]
        [HttpDelete("{id:int}")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> DeleteTest([FromRoute] int id)
        {
            return await Ok(() => _serviceTest.DeleteTestAsync(id));
        }

        [HttpGet("{id:int}/Themes")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> GetTestThemes([FromRoute] int id, [FromQuery] FilterTheme filter)
        {
            return await Ok(() => _serviceTheme.GetThemesAsync(filter.SetTestId(id)));
        }
    }
}