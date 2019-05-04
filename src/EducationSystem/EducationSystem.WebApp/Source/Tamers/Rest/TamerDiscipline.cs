using System.Collections.Generic;
using System.Threading.Tasks;
using EducationSystem.Constants;
using EducationSystem.Interfaces.Managers;
using EducationSystem.Models.Filters;
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
            _managerTest = managerTest;
            _managerTheme = managerTheme;
            _managerDiscipline = managerDiscipline;
        }

        [HttpGet]
        [Roles(UserRoles.Admin, UserRoles.Lecturer, UserRoles.Student)]
        public async Task<IActionResult> GetDisciplines([FromQuery] FilterDiscipline filter)
        {
            return await Ok(() => _managerDiscipline.GetDisciplinesAsync(filter));
        }

        [HttpGet("{id:int}")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer, UserRoles.Student)]
        public async Task<IActionResult> GetDiscipline([FromRoute] int id)
        {
            return await Ok(() => _managerDiscipline.GetDisciplineAsync(id));
        }

        [HttpGet("{id:int}/Tests")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer, UserRoles.Student)]
        public async Task<IActionResult> GetDisciplineTests([FromRoute] int id, [FromQuery] FilterTest filter)
        {
            return await Ok(() => _managerTest.GetTestsAsync(filter.SetDisciplineId(id)));
        }

        [HttpGet("{id:int}/Themes")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> GetDisciplineThemes([FromRoute] int id, [FromQuery] FilterTheme filter)
        {
            return await Ok(() => _managerTheme.GetThemesAsync(filter.SetDisciplineId(id)));
        }

        [HttpPut("{id:int}/Themes")]
        [Roles(UserRoles.Lecturer)]
        public async Task<IActionResult> UpdateDisciplineThemes([FromRoute] int id, [FromBody] List<Theme> themes)
        {
            return await Ok(() => _managerTheme.UpdateDisciplineThemesAsync(id, themes));
        }
    }
}