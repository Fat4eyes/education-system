using System.Collections.Generic;
using System.Threading.Tasks;
using EducationSystem.Constants;
using EducationSystem.Interfaces.Services;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Rest;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Rest
{
    [Route("Api/Disciplines")]
    public class TamerDiscipline : Tamer
    {
        private readonly IServiceTest _serviceTest;
        private readonly IServiceTheme _serviceTheme;
        private readonly IServiceDiscipline _serviceDiscipline;

        public TamerDiscipline(
            IServiceTest serviceTest,
            IServiceTheme serviceTheme,
            IServiceDiscipline serviceDiscipline)
        {
            _serviceTest = serviceTest;
            _serviceTheme = serviceTheme;
            _serviceDiscipline = serviceDiscipline;
        }

        [HttpGet]
        [Roles(UserRoles.Admin, UserRoles.Lecturer, UserRoles.Student)]
        public async Task<IActionResult> GetDisciplines([FromQuery] FilterDiscipline filter)
        {
            return await Ok(() => _serviceDiscipline.GetDisciplinesAsync(filter));
        }

        [HttpGet("{id:int}")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer, UserRoles.Student)]
        public async Task<IActionResult> GetDiscipline([FromRoute] int id)
        {
            return await Ok(() => _serviceDiscipline.GetDisciplineAsync(id));
        }

        [HttpGet("{id:int}/Tests")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer, UserRoles.Student)]
        public async Task<IActionResult> GetDisciplineTests([FromRoute] int id, [FromQuery] FilterTest filter)
        {
            return await Ok(() => _serviceTest.GetTestsAsync(filter.SetDisciplineId(id)));
        }

        [HttpGet("{id:int}/Themes")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> GetDisciplineThemes([FromRoute] int id, [FromQuery] FilterTheme filter)
        {
            return await Ok(() => _serviceTheme.GetThemesAsync(filter.SetDisciplineId(id)));
        }

        [HttpPut("{id:int}/Themes")]
        [Roles(UserRoles.Lecturer)]
        public async Task<IActionResult> UpdateDisciplineThemes([FromRoute] int id, [FromBody] List<Theme> themes)
        {
            return await Ok(() => _serviceTheme.UpdateDisciplineThemesAsync(id, themes));
        }
    }
}