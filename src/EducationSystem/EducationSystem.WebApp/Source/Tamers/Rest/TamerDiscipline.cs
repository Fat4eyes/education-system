using System.Collections.Generic;
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
            _managerDiscipline = managerDiscipline;
            _managerTest = managerTest;
            _managerTheme = managerTheme;
        }

        [HttpGet]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> GetDisciplines(
            [FromQuery] OptionsDiscipline options,
            [FromQuery] FilterDiscipline filter)
        {
            return Ok(await _managerDiscipline.GetDisciplines(options, filter));
        }

        [HttpGet("{id:int}")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> GetDiscipline([FromRoute] int id, [FromQuery] OptionsDiscipline options)
        {
            return Ok(await _managerDiscipline.GetDiscipline(id, options));
        }

        [HttpGet("{id:int}/Tests")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> GetTestsByDisciplineId(
            [FromRoute] int id,
            [FromQuery] OptionsTest options,
            [FromQuery] FilterTest filter)
        {
            return Ok(await _managerTest.GetTestsByDisciplineId(id, options, filter));
        }

        [HttpGet("{id:int}/Themes")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> GetThemesByDisciplineId(
            [FromRoute] int id,
            [FromQuery] OptionsTheme options,
            [FromQuery] FilterTheme filter)
        {
            return Ok(await _managerTheme.GetThemesByDisciplineId(id, options, filter));
        }

        [Transaction]
        [HttpPut("{id:int}/Themes")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> UpdateDisciplineThemes([FromRoute] int id, [FromBody] List<Theme> themes)
        {
            await _managerTheme.UpdateDisciplineThemes(id, themes);

            return Ok();
        }
    }
}