using EducationSystem.Constants.Source;
using EducationSystem.Interfaces.Managers.Rest;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Models.Source.Options;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Rest
{
    [Route("Api/Disciplines")]
    [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
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

        [HttpGet("")]
        public IActionResult GetDisciplines(
            [FromQuery] OptionsDiscipline options,
            [FromQuery] FilterDiscipline filter)
            => Ok(_managerDiscipline.GetDisciplines(options, filter));

        [HttpGet("{disciplineId:int}")]
        public IActionResult GetDiscipline(
            [FromRoute] int disciplineId,
            [FromQuery] OptionsDiscipline options)
            => Ok(_managerDiscipline.GetDisciplineById(disciplineId, options));

        [HttpGet("{disciplineId:int}/Tests")]
        public IActionResult GetDisciplineTests(
            [FromRoute] int disciplineId,
            [FromQuery] OptionsTest options,
            [FromQuery] FilterTest filter)
            => Ok(_managerTest.GetTestsByDisciplineId(disciplineId, options, filter));

        [HttpGet("{disciplineId:int}/Themes")]
        public IActionResult GetDisciplineThemes(
            [FromRoute] int disciplineId,
            [FromQuery] OptionsTheme options,
            [FromQuery] FilterTheme filter)
            => Ok(_managerTheme.GetThemesByDisciplineId(disciplineId, options, filter));
    }
}