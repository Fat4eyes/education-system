using System.Threading.Tasks;
using EducationSystem.Constants;
using EducationSystem.Interfaces.Managers;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Rest
{
    [Route("Api/Disciplines")]
    public class TamerDiscipline : Tamer
    {
        private readonly IManagerTest _managerTest;
        private readonly IManagerTheme _managerTheme;
        private readonly IManagerDiscipline _managerDiscipline;

        public TamerDiscipline(
            IManagerTest managerTest,
            IManagerTheme managerTheme,
            IManagerDiscipline managerDiscipline)
        {
            _managerTest = managerTest;
            _managerTheme = managerTheme;
            _managerDiscipline = managerDiscipline;
        }

        [HttpGet]
        [Roles(UserRoles.Admin, UserRoles.Lecturer, UserRoles.Student)]
        public async Task<IActionResult> GetDisciplines([FromQuery] OptionsDiscipline options, [FromQuery] FilterDiscipline filter)
        {
            return await Ok(() => _managerDiscipline.GetDisciplinesAsync(options, filter));
        }

        [HttpGet("{id:int}")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer, UserRoles.Student)]
        public async Task<IActionResult> GetDiscipline([FromRoute] int id, [FromQuery] OptionsDiscipline options)
        {
            return await Ok(() => _managerDiscipline.GetDisciplineAsync(id, options));
        }

        [HttpGet("{id:int}/Tests")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer, UserRoles.Student)]
        public async Task<IActionResult> GetDisciplineTests(
            [FromRoute] int id,
            [FromQuery] OptionsTest options,
            [FromQuery] FilterTest filter)
        {
            return await Ok(() => _managerTest.GetTestsAsync(options, filter.SetDisciplineId(id)));
        }

        [HttpGet("{id:int}/Themes")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer, UserRoles.Student)]
        public async Task<IActionResult> GetDisciplineThemes(
            [FromRoute] int id,
            [FromQuery] OptionsTheme options,
            [FromQuery] FilterTheme filter)
        {
            return await Ok(() => _managerTheme.GetThemesAsync(options, filter.SetDisciplineId(id)));
        }
    }
}