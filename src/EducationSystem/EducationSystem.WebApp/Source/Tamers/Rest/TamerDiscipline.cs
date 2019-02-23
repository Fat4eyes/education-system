using EducationSystem.Constants.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
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
        protected IManagerTest ManagerTest { get; }
        protected IManagerTheme ManagerTheme { get; }
        protected IManagerDiscipline ManagerDiscipline { get; }

        public TamerDiscipline(
            IManagerTest managerTest,
            IManagerTheme managerTheme,
            IManagerDiscipline managerDiscipline)
        {
            ManagerDiscipline = managerDiscipline;
            ManagerTest = managerTest;
            ManagerTheme = managerTheme;
        }

        [HttpGet("")]
        public IActionResult GetDisciplines(
            [FromQuery] OptionsDiscipline options,
            [FromQuery] Filter filter)
            => Ok(ManagerDiscipline.GetDisciplines(options, filter));

        [HttpGet("{disciplineId:int}")]
        public IActionResult GetDiscipline(
            [FromRoute] int disciplineId,
            [FromQuery] OptionsDiscipline options)
            => Ok(ManagerDiscipline.GetDisciplineById(disciplineId, options));

        [HttpGet("{disciplineId:int}/Tests")]
        public IActionResult GetDisciplineTests(
            [FromRoute] int disciplineId,
            [FromQuery] OptionsTest options,
            [FromQuery] FilterTest filter)
            => Ok(ManagerTest.GetTestsByDisciplineId(disciplineId, options, filter));

        [HttpGet("{disciplineId:int}/Themes")]
        public IActionResult GetDisciplineThemes(
            [FromRoute] int disciplineId,
            [FromQuery] OptionsTheme options,
            [FromQuery] FilterTheme filter)
            => Ok(ManagerTheme.GetThemesByDisciplineId(disciplineId, options, filter));
    }
}